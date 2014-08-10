using System.Collections.Generic;

namespace Alfursan.IRepository
{
    public interface IRepository <T>
    {
        T Get(int id);
        
        void Set(T entity);

        List<T> GetAll();

        void Update(T entity);
    }
}
