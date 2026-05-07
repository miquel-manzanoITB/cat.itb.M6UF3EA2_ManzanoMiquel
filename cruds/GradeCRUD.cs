using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.M6UF3EA2_ManzanoMiquel.connections;
using cat.itb.M6UF3EA2_ManzanoMiquel.model.Grade;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.cruds
{
    public class GradeCRUD
    {
        public void LoadGradesCollection()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("grades");
            var collection = database.GetCollection<BsonDocument>("grades");

            FileInfo file = new FileInfo("../../../files/grades.json");
            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Grade grade = JsonConvert.DeserializeObject<Grade>(line);
                    Console.WriteLine(grade.StudentId._NumberInt.ToString());
                    string json = JsonConvert.SerializeObject(grade);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }
        }
    }
}
