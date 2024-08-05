using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;


namespace RestoranoSistema.Services
{
    public class TableService : ITableService
    {
        private readonly List<Table> _tables;
        public TableService(List<Table> tables)
        {
            _tables = tables;
        }


        public Table GetTable(int tabelId)
        {
            foreach (var table in _tables)
            {
                if (table.Id == tabelId && table.IsOccupied == false)
                {
                    return table;
                }
            }
            return null;
        }
        public void MarkTableAsOccupied(int tableid)
        {
            foreach (var table in _tables)
            {
                if (table.Id == tableid)
                {
                    table.IsOccupied = true;
                }
            }
        }
    }
}
