using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6UF3EA2_ManzanoMiquel.connections;
using cat.itb.M6UF3EA2_ManzanoMiquel.model;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.cruds
{
    public class StudentCRUD
    {
        public void LoadStudentsCollection()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("students");
            var collection = database.GetCollection<BsonDocument>("students");

            FileInfo file = new FileInfo("../../../files/students.json");
            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Student student = JsonConvert.DeserializeObject<Student>(line);
                    Console.WriteLine(student.Firstname);
                    string json = JsonConvert.SerializeObject(student);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }
        }
    }
}
