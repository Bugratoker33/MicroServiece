using MongoDB.Bson.Serialization.Attributes;

namespace Stock.API.Models.Entities
{
    public class Stock
    {
    //    //mongo db veri tabanı için gerekli adımları ve attiributlerle imzalamalarımızı atıyoruz 
    //    //ıd olduğunu ve Guid olduğunu belirtiğimiz yer 
    //    [BsonId]
    //    [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.CSharpLegacy)]
    //    [BsonElement(Order = 0)]
    //    public Guid Id { get; set; }

    //    //[BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.CSharpLegacy)]
    //    //[BsonElement(Order = 1)]
    //    //public Guid ProductId { get; set; }

    //    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    //    [BsonElement(Order = 1)]
    //    public string ProductId { get; set; }

    //    [BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
    //    [BsonElement(Order = 2)]
    //    public int Count { get; set; }


        [BsonId]
        [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.CSharpLegacy)]
        [BsonElement(Order = 0)]
        public Guid Id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        [BsonElement(Order = 1)]
        public string ProductId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
        [BsonElement(Order = 2)]
        public int Count { get; set; }
    }
}
