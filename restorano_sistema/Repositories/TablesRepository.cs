using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;
using RestoranoSistema.Repositories.Interfaces;


namespace RestoranoSistema.Repositories
{
    public class TablesRepository : ITableRepository
    {
        private string _filePath;
        public TablesRepository(string filepath)
        {
            _filePath = filepath;
        }
        public List<Table> LoadTables()
        {
            List<Table> tables = new List<Table>();
            try
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var tableData = line.Split(';');
                    Table table = new Table();
                    table.Id = int.Parse(tableData[0]);
                    table.Seats = int.Parse(tableData[1]);
                    table.IsOccupied = bool.Parse(tableData[2]);
                    tables.Add(table);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Įvyko klaida nuskaitant failą: " + ex.Message);
            }
            return tables;
        }
        public void SaveTables(Table table)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Įvyko klaida išsaugant failą: " + ex.Message);
            }
        }
    }
}
