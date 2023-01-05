using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SchoolERP.Domain.DatabaseSettings;
using SchoolERP.Models.FeesPayment;
using MongoDB.Bson;

namespace SchoolERP.Services.FeesPayment
{
    public class FeesPaymentService
    {
        private readonly IMongoCollection<FeesPaymentModel> _feesPaymentCollection;

        public FeesPaymentService(IOptions<DatabaseSettings> schoolErpDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            schoolErpDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                schoolErpDatabaseSettings.Value.DatabaseName);

            _feesPaymentCollection = mongoDatabase.GetCollection<FeesPaymentModel>(
                schoolErpDatabaseSettings.Value.FeesPaymentCollectionName);
        }

        public async Task<List<FeesPaymentModel>> GetAsync() =>
        await _feesPaymentCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(FeesPaymentModel newFeePayment) =>
        await _feesPaymentCollection.InsertOneAsync(newFeePayment);

        public async Task<bool> checkIfInstallmentDetailIsPaid(string id, string installmentId,string itemMasterId)
        {   
            var _id = ObjectId.Parse(id);
            var filter = Builders<FeesPaymentModel>.Filter.Eq("_id", _id);
            var feesPayment = _feesPaymentCollection.Find(filter).FirstOrDefault();
            var filterInstallment = Builders<Installment>.Filter.Eq("_id", installmentId);
            var isPaid = feesPayment.installments.Find(i => i._id.ToString() == installmentId)
                .intallmentDetails.Where(intd => intd.itemMaserId.ToString() == itemMasterId)
                .Select(intd => intd.isPaid).FirstOrDefault();

            return isPaid;
        }
    }
}
