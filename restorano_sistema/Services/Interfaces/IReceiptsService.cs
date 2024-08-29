using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Entities;

namespace RestoranoSistema.Services.Interfaces
{
    public interface IReceiptsService
    {
        List<string> GenerateClientReceipt(Order order);
        List<string> GenerateRestaurantReceipt(Order order);
        void SendClientReceiptToEmail(List<string> receipt, string? email);
    }
}
