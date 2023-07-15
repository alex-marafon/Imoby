using Imoby.Domain.Interface.Repository;

namespace Imoby.Domain.Service;
public class ServiceBase<T> : IServiceBase<T> where T : class
{

    private IRepositoryBase<T> _repositoryBase;
    public ServiceBase(IRepositoryBase<T> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public IEnumerable<T> GetAll()
    {
        return _repositoryBase.GetAll();
    }

    public Tuple<IEnumerable<T>, int, int> GetAllPaginat(int pag, int volume)
    {
        return _repositoryBase.GetAllPaginat(pag, volume);
    }

    public T GetById(string id)
    {
        return _repositoryBase.GetById(id);
    }

    public void Add(T obj)
    {
        _repositoryBase.Add(obj);
    }

    public void Update(string id, T obj)
    {
        _repositoryBase.Update(id, obj);
    }

    public void Delete(string id)
    {
        _repositoryBase.Delete(id);
    }
}
