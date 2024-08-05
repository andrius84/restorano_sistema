using RestoranoSistema.Services;

namespace Projektas_restorano_sistema
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRestaurant restaurant = new Restaurant();
            restaurant.Start();

        }
    }
}
