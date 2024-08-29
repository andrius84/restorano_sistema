using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;

namespace RestoranoSistema.Repositories.Interfaces
{
    public interface ITableRepository
    {
        List<Table> LoadTables();
        void SaveTables(Table table);

    }
}
