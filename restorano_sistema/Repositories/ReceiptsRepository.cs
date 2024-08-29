using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;
using RestoranoSistema.Repositories.Interfaces;


namespace RestoranoSistema.Repositories
{
    public class ReceiptsRepository : IReceiptRepository
    {
        private readonly string _filePath;

        public ReceiptsRepository(string filePath)
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
