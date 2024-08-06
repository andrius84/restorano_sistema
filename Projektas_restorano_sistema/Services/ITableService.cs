using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Services
{
    public interface ITableService
    {
        Table GetTable(int tableId);
        void MarkTableAsOccupied(int tableid);
        void ChooseTable(int tableId);
    }
}
