using MongoDB.Driver;

namespace MongoDB_Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> GetAll();
        Task Update(string id, TEntity entity);
        Task Remove(string id);
        Task AddMany(List<TEntity> entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        readonly IMongoCollection<TEntity> _collection;

        public Repository(string conn, string db, string collection) 
        {
            var mongoClient = new MongoClient(conn);

            var mongoDatabase = mongoClient.GetDatabase(db);

            _collection = mongoDatabase.GetCollection<TEntity>(collection);
        }

        public async Task Add(TEntity entity) => 
            await _collection!.InsertOneAsync(entity);
        public async Task AddMany(List<TEntity> entity) => await _collection!.InsertManyAsync(entity);

        public async Task<List<TEntity>> GetAll() =>
            await _collection!.Find(_ => true).ToListAsync();

        public async Task<TEntity> GetById(string id)=>
            await _collection!.Find(Builders<TEntity>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        public async Task Remove(string id) => 
            await _collection!.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        public async Task Update(string id, TEntity entity) =>
            await _collection!.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id",id),entity);
       }
    }