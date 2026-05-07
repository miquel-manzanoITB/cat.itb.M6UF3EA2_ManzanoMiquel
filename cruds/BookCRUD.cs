using System;
using System.Collections.Generic;
using cat.itb.M6UF3EA2_ManzanoMiquel.connections;
using cat.itb.M6UF3EA2_ManzanoMiquel.model.Book;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.cruds
{
    public class BookCRUD
    {
        // Ejercicio 1 - Importar colección de libros
        public void LoadBooksCollection()
        {
            FileInfo file = new FileInfo("../../../files/books.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();

            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("books");
            var collection = database.GetCollection<BsonDocument>("books");

            if (books != null)
                foreach (var book in books)
                {
                    Console.WriteLine($"  Importando: {book.Title}");
                    string json = JsonConvert.SerializeObject(book);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            Console.WriteLine($">> Coleccion 'books' importada con {books?.Count} documentos.");
        }

        // Ejercicio 2c - Mostrar titulo, paginas y categorias ordenados por paginas desc
        public void GetBooksTitlePagesCategoriesOrderedByPages()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var projection = Builders<BsonDocument>.Projection
                .Include("title").Include("pageCount").Include("categories").Exclude("_id");
            var sort = Builders<BsonDocument>.Sort.Descending("pageCount");

            var books = collection.Find(new BsonDocument()).Project(projection).Sort(sort).ToList();

            Console.WriteLine($"\n>> Libros ordenados por paginas (desc): {books.Count} resultados");
            foreach (var book in books)
            {
                string title = book.GetValue("title", "N/A").AsString;
                int pages = book.GetValue("pageCount", 0).ToInt32();
                string cats = book.Contains("categories") ? string.Join(", ", book["categories"].AsBsonArray) : "[]";
                Console.WriteLine($"  [{pages} pag] {title} | Categorias: {cats}");
            }
        }

        // Ejercicio 2f - Mostrar title, pageCount y autores de libros con menos de 130 paginas
        public void GetBooksUnder130Pages()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.Lt("pageCount", 130);
            var projection = Builders<BsonDocument>.Projection
                .Include("title").Include("pageCount").Include("authors").Exclude("_id");

            var books = collection.Find(filter).Project(projection).ToList();

            Console.WriteLine($"\n>> Libros con menos de 130 paginas: {books.Count} resultados");
            foreach (var book in books)
            {
                string title = book.GetValue("title", "N/A").AsString;
                int pages = book.GetValue("pageCount", 0).ToInt32();
                string authors = book.Contains("authors") ? string.Join(", ", book["authors"].AsBsonArray) : "[]";
                Console.WriteLine($"  Titulo: {title} | Paginas: {pages} | Autores: {authors}");
            }
        }

        // Ejercicio 3c - Añadir autor "Sam Watters" al libro "Code Generation in Action"
        public void AddAuthorToCodeGenerationBook()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.Eq("title", "Code Generation in Action");
            var before = collection.Find(filter).FirstOrDefault();
            if (before == null) { Console.WriteLine(">> Libro no encontrado."); return; }

            Console.WriteLine($"\n>> Autores ANTES: {string.Join(", ", before["authors"].AsBsonArray)}");

            // Añadir "Sam Watters" al array de autores
            var update = Builders<BsonDocument>.Update.Push("authors", "Sam Watters");
            collection.UpdateOne(filter, update);

            var after = collection.Find(filter).FirstOrDefault();
            Console.WriteLine($">> Autores DESPUES: {string.Join(", ", after["authors"].AsBsonArray)}");
        }

        // Ejercicio 4c - Eliminar libros con paginas entre 0 y 100
        public void DeleteBooksUnder100Pages()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gt("pageCount", 0),
                Builders<BsonDocument>.Filter.Lte("pageCount", 100)
            );

            var result = collection.DeleteMany(filter);
            Console.WriteLine($"\n>> Libros eliminados (paginas entre 0 y 100): {result.DeletedCount}");
        }

        // Ejercicio 4e - Eliminar la ultima categoria del libro con ISBN 1933988177
        public void DeleteLastCategoryFromBookByISBN()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var filter = Builders<BsonDocument>.Filter.Eq("isbn", "1933988177");
            var book = collection.Find(filter).FirstOrDefault();
            if (book == null) { Console.WriteLine(">> Libro con ISBN 1933988177 no encontrado."); return; }

            Console.WriteLine($">> Categorias ANTES: {string.Join(", ", book["categories"].AsBsonArray)}");

            // $pop con PopLast elimina el ultimo elemento
            var update = Builders<BsonDocument>.Update.PopLast("categories");
            collection.UpdateOne(filter, update);

            var updated = collection.Find(filter).FirstOrDefault();
            Console.WriteLine($">> Categorias DESPUES: {string.Join(", ", updated["categories"].AsBsonArray)}");
        }
    }
}
