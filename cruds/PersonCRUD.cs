using System;
using System.Collections.Generic;
using cat.itb.M6UF3EA2_ManzanoMiquel.connections;
using cat.itb.M6UF3EA2_ManzanoMiquel.model.Person;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.cruds
{
    public class PersonCRUD
    {
        // Ejercicio 1 - Importar coleccion de personas
        public void LoadPeopleCollection()
        {
            FileInfo file = new FileInfo("../../../files/people.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();

            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("people");
            var collection = database.GetCollection<BsonDocument>("people");

            if (people != null)
                foreach (var person in people)
                {
                    Console.WriteLine($"  Importando: {person.Name}");
                    string json = JsonConvert.SerializeObject(person);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            Console.WriteLine($">> Coleccion 'people' importada con {people?.Count} documentos.");
        }

        // Ejercicio 2g - Mostrar los nombres de los amigos de Caroline Webster
        public void GetFriendsOfCarolineWebster()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "Caroline Webster");
            var projection = Builders<BsonDocument>.Projection
                .Include("friends").Exclude("_id");

            var person = collection.Find(filter).Project(projection).FirstOrDefault();
            if (person == null) { Console.WriteLine(">> Caroline Webster no encontrada."); return; }

            Console.WriteLine("\n>> Amigos de Caroline Webster:");
            if (person.Contains("friends") && person["friends"].IsBsonArray)
            {
                foreach (BsonDocument friend in person["friends"].AsBsonArray)
                    Console.WriteLine($"  - {friend.GetValue("name", "N/A")}");
            }
            else
                Console.WriteLine("  (Sin amigos registrados)");
        }

        // Ejercicio 4g - Eliminar el campo "tags" de todos los profesores "teacher"
        public void DeleteTagsFromTeachers()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("type", "teacher");

            // $unset elimina el campo tags
            var update = Builders<BsonDocument>.Update.Unset("tags");
            var result = collection.UpdateMany(filter, update);
            Console.WriteLine($"\n>> Campo 'tags' eliminado de {result.ModifiedCount} profesores.");
        }
    }
}
