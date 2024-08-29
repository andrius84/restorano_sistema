using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace RestoranoSistema.Repositories
{
    public class TablesRepository : ITableRepository
    {
        private readonly RestoranasDbContext _context;

        public TablesRepository(RestoranasDbContext context)
        {
            _context = context;
        }

        public List<Table> LoadTables()
        {
            try
            {
                return _context.Tables.ToList(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while loading tables from the database: " + ex.Message);
                return new List<Table>();
            }
        }

        public void SaveTables(Table table)
        {
            try
            {
                var existingTable = _context.Tables.FirstOrDefault(t => t.Id == table.Id);
                if (existingTable != null)
                {
                    existingTable.Seats = table.Seats;
                    existingTable.IsOccupied = table.IsOccupied;
                    _context.Tables.Update(existingTable);
                }
                else
                {
                    _context.Tables.Add(table);
                }
                _context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while saving the table to the database: " + ex.Message);
            }
        }
    }
}
