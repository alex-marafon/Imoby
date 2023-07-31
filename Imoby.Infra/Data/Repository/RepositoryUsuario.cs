using Imoby.Domain.Interface.Repository;
using Imoby.Entities.Entitie.Models;
using MongoDB.Driver;

namespace Imoby.Infra.Data.Repository;
public class RepositoryUsuario : RepositoryBase<Usuario>, IRepositoryUsuario
{
    public bool GetEmailExist(string email)
    {
        var filter = Builders<Usuario>.Filter.Where(x =>x.Email == email);
        var result = databaseContext.GetCollection<Usuario>().Find(filter).FirstOrDefault();
        return result != null ? true : false;
    }
}
