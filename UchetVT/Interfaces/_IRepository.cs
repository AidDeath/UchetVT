using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace UchetVT
{
    /// <summary>
    /// Интерфейс для репозиториев (в основногм применяется к справочникам)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        ObservableCollection<T> GetAll();
        T Get(int id);
        void Set(T model);
        void Update(T model);
        void Delete(T model);
        GridView MakeView();
    }
}
