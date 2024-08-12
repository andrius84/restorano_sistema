using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Repositories
{
    public interface IReceiptRepository
    {
        void SaveClientReceiptToFile(List<string> receiptLines);
        void SaveRestaurantReceiptToFile(List<string> receiptLines);
    }
}
