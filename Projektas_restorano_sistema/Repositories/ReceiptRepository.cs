using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;


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
            File.WriteAllLines(_filePath, receiptLines);
        }

        public void SaveRestaurantReceiptToFile(List<string> receiptLines)
        {
            File.WriteAllLines(_filePath, receiptLines);
        }
    }
}
