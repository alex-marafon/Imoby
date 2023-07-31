using Imoby.Domain.Interface.Repository;
using Imoby.Domain.Interface.Service;
using Imoby.Entities.Entitie.Models;

namespace Imoby.Domain.Service;
public class ServiceUsuario : ServiceBase<Usuario> , IServiceUsuario
{
    private readonly IRepositoryUsuario _repositoryUsuario;
    public ServiceUsuario(IRepositoryUsuario repositoryBase) : base(repositoryBase)
    {
        _repositoryUsuario = repositoryBase;
    }


    public bool GetEmailExist(string email)
    {
       return _repositoryUsuario.GetEmailExist(email);
    }
}
