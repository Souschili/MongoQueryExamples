using MongoDB.Driver;

namespace MongoQueryExamples.Mongo
{
    class ApplicationContext
    {
        public IMongoCollection<object> PersonCollection { get; set; }

        public ApplicationContext()
        {
            IMongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("TestQueryDB");
            this.PersonCollection = database.GetCollection<object>("Persons");
        }
    }
}
