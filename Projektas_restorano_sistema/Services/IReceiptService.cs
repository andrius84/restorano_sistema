using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Services
{
    public interface IReceiptService
    {
        List<string> GenerateClientReceipt(Order order);
        List<string> GenerateRestaurantReceipt(Order order);
    }
}
