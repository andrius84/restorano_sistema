using RestoranoSistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.UI.Interfaces
{
    public interface IUserInterface
    {
        void ShowMainMenu();
        Guid CreateOrder();
        void UpdateOrder(Guid orderId);
        List<MenuItem> GetMenuItems();
    }
}
