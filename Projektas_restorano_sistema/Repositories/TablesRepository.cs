using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Repositories
{
    public class TablesRepository
    {
        private string _filePath;
        private List<Table> _tables;
        public TablesRepository(List<Table> tables, string filepath)
        {
            _filePath = filepath;
            _tables = tables;
        }
        public List<Table> LoadTablesList()
        {
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var values = line.Split(';');
                var table = new Table
                {
                    Id = int.Parse(values[0]),
                    Seats = int.Parse(values[1]),
                    IsOccupied = bool.Parse(values[2])
                };
                _tables.Add(table);
            }
            return _tables;
        }
        public void SaveTablesList()
        {
            var lines = new List<string>();
            foreach (var table in _tables)
            {
                var line = $"{table.Id};{table.Seats};{table.IsOccupied}";
                lines.Add(line);
            }
            File.WriteAllLines(_filePath, lines);
        }
    }
}
