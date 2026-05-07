using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cat.itb.M6UF3EA2_ManzanoMiquel.model.Book
{
    [Serializable]
    public class Book
    {
        [JsonProperty("_id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }

        [JsonProperty("publishedDate")]
        public PublishedDate PublishedDate { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("longDescription")]
        public string LongDescription { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("authors")]
        public List<string> Authors { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }

        public override string ToString()
        {
            return
                "Book{" +
                "Id = '" + Id + '\'' +
                ",Title = '" + Title + '\'' +
                ",Isbn = '" + Isbn + '\'' +
                ",PageCount = '" + PageCount + '\'' +
                ",PublishedDate = '" + PublishedDate + '\'' +
                ",ThumbnailUrl = '" + ThumbnailUrl + '\'' +
                ",ShortDescription = '" + ShortDescription + '\'' +
                ",LongDescription = '" + LongDescription + '\'' +
                ",Status = '" + Status + '\'' +
                ",Authors = '" + Authors + '\'' +
                ",Categories = '" + Categories + '\'' +
                "}";
        }
    }
}
