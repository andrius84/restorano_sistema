﻿namespace RestoranoSistema
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
