using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;
using RestoranoSistema.Repositories;


namespace RestoranoSistema.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly Table _table;
        public TableService(Table table, ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
            _table = table;
        }
        public Table GetTable(int tableId)
        {
            var tables = _tableRepository.LoadTables();
            foreach (var table in tables)
            {
                var tableData = table.Split(';');
                if (int.Parse(tableData[0]) == tableId)
                {
                    _table.Id = int.Parse(tableData[0]);
                    _table.Seats = int.Parse(tableData[1]);
                    _table.IsOccupied = bool.Parse(tableData[2]);
                    return _table;
                }
            }
            return null;
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
