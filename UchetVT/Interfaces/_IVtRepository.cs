using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace UchetVT
{/// <summary>
/// интерфейс для репозиториев с префиксом VT, в основном техника 
/// </summary>
/// <typeparam name="T"></typeparam>
    public interface IVtRepository<T>
    {
        ObservableCollection<T> GetAll(int regionId);
        void Set(T model);
        void Update(T model);
        void Delete(T model);
        GridView MakeView();
    }
}
