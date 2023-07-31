using Imoby.Domain.Interface.Repository;
using Imoby.Entities.Entitie.Models;

namespace Imoby.Domain.Interface.Service;
public interface IServiceUsuario : IServiceBase<Usuario>
{
    bool GetEmailExist(string email);

}
