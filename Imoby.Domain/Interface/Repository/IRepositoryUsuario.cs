using Imoby.Entities.Entitie.Models;

namespace Imoby.Domain.Interface.Repository;
public interface IRepositoryUsuario : IRepositoryBase<Usuario>
{
    bool GetEmailExist(string email);
}
