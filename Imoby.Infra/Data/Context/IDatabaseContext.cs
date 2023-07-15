using MongoDB.Driver;

namespace Imoby.Infra.Data.Context;
public interface IDatabaseContext
    {
        IMongoDatabase Database { get; set; }

        IMongoCollection<T> GetCollection<T>();
    }
