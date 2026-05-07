using System;
using System.Collections.Generic;
using cat.itb.M6UF3EA2_ManzanoMiquel.connections;
using cat.itb.M6UF3EA2_ManzanoMiquel.model.Restaurant;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.cruds
{
    public class RestaurantCRUD
    {
        // Ejercicio 1 - Importar coleccion de restaurantes
        public void LoadRestaurantsCollection()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("restaurants");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            FileInfo file = new FileInfo("../../../files/restaurants.json");
            int count = 0;
            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(line);
                    string json = JsonConvert.SerializeObject(restaurant);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                    count++;
                }
            }
            Console.WriteLine($">> Coleccion 'restaurants' importada con {count} documentos.");
        }

        // Ejercicio 2d - Mostrar nombre y tipo de cocina por zipcode (parametro)
        public void GetRestaurantsByZipcode(string zipcode)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            // Filtrar por address.zipcode
            var filter = Builders<BsonDocument>.Filter.Eq("address.zipcode", zipcode);
            var projection = Builders<BsonDocument>.Projection
                .Include("name").Include("cuisine").Exclude("_id");

            var restaurants = collection.Find(filter).Project(projection).ToList();

            Console.WriteLine($"\n>> Restaurantes con zipcode {zipcode}: {restaurants.Count} resultados");
            foreach (var r in restaurants)
                Console.WriteLine($"  Nombre: {r.GetValue("name", "N/A")} | Cocina: {r.GetValue("cuisine", "N/A")}");
        }

        // Ejercicio 2e - Mostrar todos los datos de restaurantes en Bronx con cocina Chinese
        public void GetBronxChineseRestaurants()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("borough", "Bronx"),
                Builders<BsonDocument>.Filter.Eq("cuisine", "Chinese")
            );

            var restaurants = collection.Find(filter).ToList();
            Console.WriteLine($"\n>> Restaurantes en Bronx con cocina Chinese: {restaurants.Count} resultados");
            foreach (var r in restaurants)
                Console.WriteLine($"  {r.ToJson()}");
        }

        // Ejercicio 3a - Actualizar zipcode del restaurante en "Driggs Avenue" a "10443"
        public void UpdateZipcodeOfDriggsAvenue()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("address.street", "Driggs Avenue");

            // Mostrar zipcode ANTES
            var before = collection.Find(filter).FirstOrDefault();
            if (before == null) { Console.WriteLine(">> Restaurante en Driggs Avenue no encontrado."); return; }
            Console.WriteLine($"\n>> Zipcode ANTES: {before["address"]["zipcode"]}");

            // Actualizar zipcode
            var update = Builders<BsonDocument>.Update.Set("address.zipcode", "10443");
            collection.UpdateOne(filter, update);

            // Mostrar zipcode DESPUES
            var after = collection.Find(filter).FirstOrDefault();
            Console.WriteLine($">> Zipcode DESPUES: {after["address"]["zipcode"]}");
        }

        // Ejercicio 4a - Eliminar todos los restaurantes del barrio Manhattan
        public void DeleteManhattanRestaurants()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");
            var result = collection.DeleteMany(filter);
            Console.WriteLine($"\n>> Restaurantes de Manhattan eliminados: {result.DeletedCount}");
        }
    }
}
