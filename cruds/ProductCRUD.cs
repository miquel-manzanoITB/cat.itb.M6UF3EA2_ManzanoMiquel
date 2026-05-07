using System;
using System.Collections.Generic;
using cat.itb.M6UF3EA2_ManzanoMiquel.connections;
using cat.itb.M6UF3EA2_ManzanoMiquel.model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.cruds
{
    public class ProductCRUD
    {
        // Ejercicio 1 - Importar coleccion de productos
        public void LoadProductsCollection()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("products");
            var collection = database.GetCollection<BsonDocument>("products");

            FileInfo file = new FileInfo("../../../files/products.json");
            int count = 0;
            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = JsonConvert.DeserializeObject<Product>(line);
                    string json = JsonConvert.SerializeObject(product);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                    count++;
                }
            }
            Console.WriteLine($">> Coleccion 'products' importada con {count} documentos.");
        }

        // Ejercicio 3b - Añadir campo "stockminim"=20 a productos con precio > 2000
        public void AddStockMinimToExpensiveProducts()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Gt("price", 2000);
            var update = Builders<BsonDocument>.Update.Set("stockminim", 20);

            var result = collection.UpdateMany(filter, update);
            Console.WriteLine($"\n>> Documentos actualizados con 'stockminim'=20: {result.ModifiedCount}");

            // Mostrar todos los documentos actualizados
            var updatedDocs = collection.Find(filter).ToList();
            Console.WriteLine(">> Documentos actualizados:");
            foreach (var doc in updatedDocs)
                Console.WriteLine($"  {doc.ToJson()}");
        }

        // Ejercicio 3d - Añadir campo "gama" segun el precio del producto
        public void AddGamaFieldToAllProducts()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            // gama "baixa": precio entre 1 y 500
            var filterBaixa = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("price", 1),
                Builders<BsonDocument>.Filter.Lte("price", 500)
            );
            var resBaixa = collection.UpdateMany(filterBaixa, Builders<BsonDocument>.Update.Set("gama", "baixa"));
            Console.WriteLine($"\n>> Productos con gama 'baixa': {resBaixa.ModifiedCount}");

            // gama "mitja": precio entre 501 y 2000
            var filterMitja = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("price", 501),
                Builders<BsonDocument>.Filter.Lte("price", 2000)
            );
            var resMitja = collection.UpdateMany(filterMitja, Builders<BsonDocument>.Update.Set("gama", "mitja"));
            Console.WriteLine($">> Productos con gama 'mitja': {resMitja.ModifiedCount}");

            // gama "extra": precio > 2000
            var filterExtra = Builders<BsonDocument>.Filter.Gt("price", 2000);
            var resExtra = collection.UpdateMany(filterExtra, Builders<BsonDocument>.Update.Set("gama", "extra"));
            Console.WriteLine($">> Productos con gama 'extra': {resExtra.ModifiedCount}");
        }

        // Ejercicio 3e - Modificar categoria "notebook" por "ipad" en MacBook Pro
        public void UpdateProductCategory(string productName, string oldCategory, string newCategory)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("name", productName);
            var before = collection.Find(filter).FirstOrDefault();
            if (before == null) { Console.WriteLine($">> Producto '{productName}' no encontrado."); return; }

            Console.WriteLine($"\n>> Categorias ANTES: {before["categories"].ToJson()}");

            // $ posicional: busca el elemento que coincide con oldCategory y lo reemplaza
            var filterWithPos = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("name", productName),
                Builders<BsonDocument>.Filter.Eq("categories", oldCategory)
            );
            var update = Builders<BsonDocument>.Update.Set("categories.$", newCategory);
            collection.UpdateOne(filterWithPos, update);

            var after = collection.Find(filter).FirstOrDefault();
            Console.WriteLine($">> Categorias DESPUES: {after["categories"].ToJson()}");
        }

        // Ejercicio 3f - Actualizar stock a 60 para productos entre 800 y 1000
        public void UpdateStockForProductsBetween800And1000()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("price", 800),
                Builders<BsonDocument>.Filter.Lte("price", 1000)
            );

            var update = Builders<BsonDocument>.Update.Set("stock", 60);
            var result = collection.UpdateMany(filter, update);
            Console.WriteLine($"\n>> Documentos actualizados (stock=60, precio 800-1000): {result.ModifiedCount}");

            var updatedDocs = collection.Find(filter).ToList();
            Console.WriteLine(">> Documentos actualizados:");
            foreach (var doc in updatedDocs)
                Console.WriteLine($"  {doc.ToJson()}");
        }

        // Ejercicio 4b - Eliminar la primera categoria del producto "iPhone 7"
        public void DeleteFirstCategoryFromIphone7()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "iPhone 7");
            var product = collection.Find(filter).FirstOrDefault();
            if (product == null) { Console.WriteLine(">> Producto 'iPhone 7' no encontrado."); return; }

            Console.WriteLine($"\n>> Categorias ANTES: {product["categories"].ToJson()}");

            // $pop con PopFirst elimina el primer elemento del array
            var update = Builders<BsonDocument>.Update.PopFirst("categories");
            collection.UpdateOne(filter, update);

            var updated = collection.Find(filter).FirstOrDefault();
            Console.WriteLine($">> Categorias DESPUES: {updated["categories"].ToJson()}");
        }

        // Ejercicio 4d - Eliminar el producto "Apple TV"
        public void DeleteAppleTV()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "Apple TV");
            var result = collection.DeleteOne(filter);
            Console.WriteLine($"\n>> Producto 'Apple TV' eliminado: {result.DeletedCount} documento(s) borrado(s).");
        }

        // Ejercicio 4f - Eliminar todos los productos con categoria "phone"
        public void DeleteProductsWithPhoneCategory()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Eq("categories", "phone");
            var result = collection.DeleteMany(filter);
            Console.WriteLine($"\n>> Productos con categoria 'phone' eliminados: {result.DeletedCount}");
        }
    }
}
