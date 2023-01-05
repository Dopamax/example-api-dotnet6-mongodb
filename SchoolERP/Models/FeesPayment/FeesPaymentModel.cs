using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SchoolERP.Models.FeesPayment
{
    public class FeesPaymentModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public ObjectId _sectionId { get; set; }
        public List<Installment> installments { get; set; }
    }

    public class Installment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string receipyNumber { get; set; }
        public bool isPaid { get; set; }
        public List<IntallmentDetail> intallmentDetails { get; set; }

    }

    public class IntallmentDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public ObjectId itemMaserId { get; set; }
        public bool isPaid { get; set; }
        public double amount { get; set; }
    }
}
