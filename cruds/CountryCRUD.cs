using System;
using System.Collections.Generic;
using cat.itb.M6UF3EA2_ManzanoMiquel.connections;
using cat.itb.M6UF3EA2_ManzanoMiquel.model.Country;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.cruds
{
    public class CountryCRUD
    {
        // Ejercicio 1 - Importar coleccion de paises
        public void LoadCountriesCollection()
        {
            FileInfo file = new FileInfo("../../../files/countries.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();

            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("countries");
            var collection = database.GetCollection<BsonDocument>("countries");

            if (countries != null)
                foreach (var country in countries)
                {
                    Console.WriteLine($"  Importando: {country.Name}");
                    string json = JsonConvert.SerializeObject(country);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            Console.WriteLine($">> Coleccion 'countries' importada con {countries?.Count} documentos.");
        }

        // Ejercicio 2a - Contar y mostrar la poblacion total de los paises de Europa
        public void GetTotalPopulationEurope()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("countries");

            // Filtrar paises europeos
            var filter = Builders<BsonDocument>.Filter.Eq("region", "Europe");
            var countries = collection.Find(filter).ToList();

            long totalPopulation = 0;
            int count = 0;
            foreach (var country in countries)
            {
                if (country.Contains("population") && !country["population"].IsBsonNull)
                {
                    totalPopulation += country["population"].ToInt64();
                    count++;
                }
            }
            Console.WriteLine($"\n>> Paises de Europa encontrados: {count}");
            Console.WriteLine($">> Poblacion total de Europa: {totalPopulation:N0} habitantes");
        }

        // Ejercicio 2b - Mostrar capital, poblacion y latlng de Madagascar
        public void GetCountryDetails(string country)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("countries");

            var filter = Builders<BsonDocument>.Filter.Eq("name", country);
            var projection = Builders<BsonDocument>.Projection
                .Include("capital").Include("population").Include("latlng").Exclude("_id");

            var madagascar = collection.Find(filter).Project(projection).FirstOrDefault();
            if (madagascar == null) { Console.WriteLine(">> Madagascar no encontrado."); return; }

            Console.WriteLine($"\n>> Madagascar:");
            Console.WriteLine($"   Capital: {madagascar.GetValue("capital", "N/A")}");
            Console.WriteLine($"   Poblacion: {madagascar.GetValue("population", 0).ToInt64():N0}");
            Console.WriteLine($"   LatLng: {madagascar["latlng"].ToJson()}");
        }

        // Ejercicio 3g - Añadir codigo 356 al campo callingCodes de Iceland
        public void AddCallingCodeToIceland()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("countries");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "Iceland");
            var before = collection.Find(filter).FirstOrDefault();
            if (before == null) { Console.WriteLine(">> Iceland no encontrado."); return; }

            Console.WriteLine($"\n>> callingCodes ANTES: {before["callingCodes"].ToJson()}");

            // Añadir codigo "356" al array callingCodes
            var update = Builders<BsonDocument>.Update.Push("callingCodes", "356");
            collection.UpdateOne(filter, update);

            var after = collection.Find(filter).FirstOrDefault();
            Console.WriteLine($">> callingCodes DESPUES: {after["callingCodes"].ToJson()}");
        }

        // Ejercicio 4h - Eliminar todos los paises donde se hable Español
        public void DeleteSpanishSpeakingCountries()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("countries");

            // Buscar paises cuyo array 'languages' contiene un objeto con name = "Spanish"
            var filter = Builders<BsonDocument>.Filter.ElemMatch<BsonDocument>(
                "languages",
                Builders<BsonDocument>.Filter.Eq("name", "Spanish")
            );

            // Mostrar cuantos seran eliminados
            var count = collection.CountDocuments(filter);
            Console.WriteLine($"\n>> Paises de habla española a eliminar: {count}");

            var result = collection.DeleteMany(filter);
            Console.WriteLine($">> Documentos eliminados: {result.DeletedCount}");
        }
    }
}
