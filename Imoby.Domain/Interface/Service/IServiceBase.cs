namespace Imoby.Domain.Interface.Repository;
public interface IServiceBase<T> where T : class
{
    IEnumerable<T> GetAll();
    Tuple<IEnumerable<T>, int, int> GetAllPaginat(int pag, int volume);
    T GetById(string id);
    void Add(T obj);
    void Update(string id, T obj);
    void Delete(string id);
}
