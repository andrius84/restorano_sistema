using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;

namespace RestoranoSistema.Services.Interfaces
{
    public interface ITablesService
    {
        Table GetTable(int tableId);
        void MarkTableAsOccupied(int tableid);
        void MarkTableAsFree(int tableid);
        void ChooseTable(int tableId);
    }
}
