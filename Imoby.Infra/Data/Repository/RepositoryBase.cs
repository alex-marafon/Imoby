using Imoby.Domain.Interface.Repository;
using Imoby.Infra.Data.Context;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Imoby.Infra.Data.Repository;
public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected IDatabaseContext databaseContext;
    public RepositoryBase(IDatabaseContext db)
    {
        this.databaseContext = db;
    }
    public RepositoryBase()
    {
        databaseContext = new DatabaseContext();
    }





    public void Add(T obj)
    {
        databaseContext.Database.GetCollection<T>(typeof(T).Name).InsertOne(obj);
    }

    public T GetById(string id)
    {
        var filter = id == null ? Builders<T>.Filter.Eq("_id", new ObjectId()) : Builders<T>.Filter.Eq("_id", new ObjectId(id));
        if (id != null)
        {
            filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        }
        var collection = databaseContext.Database.GetCollection<T>(typeof(T).Name);
        return collection.Find(filter).FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
        var result = databaseContext.GetCollection<T>().AsQueryable().ToList();
        return result;
    }

    public Tuple<IEnumerable<T>, int, int> GetAllPaginat(int pag, int volume)
    {
        if (pag <= 0)
            pag = 1;

        if (volume < 30)
            volume = 30;

        if (volume > 100)
            volume = 100;

        int pags = ((pag * volume) - volume);

        var result = databaseContext.GetCollection<T>().AsQueryable().Skip(pags).Take(volume);

        var totalItens = databaseContext.GetCollection<T>().AsQueryable().Count();
        var pages = totalItens % volume;
        var totalPaginas = pages > 0 ? (totalItens / volume) + 1 : (totalItens / volume);

        return new Tuple<IEnumerable<T>, int, int>(result, totalPaginas, totalItens);

    }

    public IEnumerable<T> GetAll(string clientId)
    {
        var filter = Builders<T>.Filter.Eq("ClienteId", clientId);
        var collection = databaseContext.Database.GetCollection<T>(typeof(T).Name);
        return collection.Find(filter).ToList();
    }

    public void Update(string id, T obj)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        var collection = databaseContext.Database.GetCollection<T>(typeof(T).Name);
        collection.ReplaceOne(filter, obj);

        //Outras formas de atualizar (Neste caso estamos atualizando o status de um tible price baseado no filtro)
        //var filter = Builders<TablePrice>.Filter.Where(x => x.ProviderId == providerId && x.TablePriceDefault == true && x.StatusTablePrice == antigo);
        //var collection = databaseContext.GetCollection<TablePrice>();
        //collection.UpdateMany(filter, Builders<TablePrice>.Update.Set(x => x.StatusTablePrice, novo));
    }

    public void Delete(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        databaseContext.Database.GetCollection<T>(typeof(T).Name).DeleteOne(filter);
    }
}
