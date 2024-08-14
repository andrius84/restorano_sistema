using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;
using RestoranoSistema.Repositories.Interfaces;


namespace RestoranoSistema.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly string _filePath;

        public ReceiptRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveClientReceiptToFile(List<string> receiptLines)
        {
            try
            {
                File.AppendAllLines(_filePath, receiptLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Įvyko klaida išsaugant kliento kvitą: {ex.Message}");
            }
        }

        public void SaveRestaurantReceiptToFile(List<string> receiptLines)
        {
            try
            {
                File.AppendAllLines(_filePath, receiptLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Įvyko klaida išsaugant restorano kvitą: {ex.Message}");
            }
        }
    }
}
