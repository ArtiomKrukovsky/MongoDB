using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Entities
{
    public class Client
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
    }
}