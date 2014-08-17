using System.Collections.Generic;
using Alfursan.Domain;

namespace Alfursan.IRepository
{
    public interface IRepository<T>
    {
        EntityResponder<T> Get(int id);

        Responder Set(T entity);

        EntityResponder<List<T>> GetAll();

        Responder Update(T entity);

        Responder Delete(int id);
    }
}
