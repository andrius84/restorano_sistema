using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;
using RestoranoSistema.Repositories;


namespace RestoranoSistema.Services
{
    public class TablesService : ITablesService
    {
        private readonly ITableRepository _tableRepository;
        public TablesService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public Table GetTable(int tableId)
        {
            var tables = _tableRepository.LoadTables();
            var table = tables.FirstOrDefault(t => t.Id == tableId);
            return table;
        }
        public void MarkTableAsOccupied(int tableid)
        {
            var table = GetTable(tableid);
            if (table != null)
            {
                table.IsOccupied = true;
                _tableRepository.SaveTables(table);
            }
        }
        public void MarkTableAsFree(int tableid)
        {
            var table = GetTable(tableid);
            if (table != null)
            {
                table.IsOccupied = false;
                _tableRepository.SaveTables(table);
            }
        }
        public void ChooseTable(int tableId)
        {
            var table = GetTable(tableId);
            if (table != null)
            {
                MarkTableAsOccupied(tableId);
            }
        }
    }
}
