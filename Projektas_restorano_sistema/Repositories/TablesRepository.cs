using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;


namespace RestoranoSistema.Repositories
{
    public class TablesRepository : ITableRepository
    { 
        private string _filePath;
        private Table _table;
        public TablesRepository(Table table, string filepath)
        {
            _filePath = filepath;
            _table = table;
        }
        public string[] LoadTables()
        {
            var lines = File.ReadAllLines(_filePath);
            return lines;  
        }
        public void SaveTables(Table table)
        {
            var lines = File.ReadAllLines(_filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                var tableData = lines[i].Split(';');
                if (int.Parse(tableData[0]) == table.Id)
                {
                    lines[i] = table.Id + ";" + table.Seats + ";" + table.IsOccupied;
                    break;
                }
            }
            File.WriteAllLines(_filePath, lines);
        }
    }
}
