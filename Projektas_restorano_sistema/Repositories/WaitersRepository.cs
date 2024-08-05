using RestoranoSistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Repositories
{
    public class WaitersRepository
    {
        private readonly string _filePath;
        private readonly List<Waiter> _waiters;
        public WaitersRepository(List<Waiter> waiters, string filePath)
        {
            _filePath = filePath;
            _waiters = waiters;
        }
        public List<Waiter> LoadWaitersList()
        {
            string[] lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                Waiter waiter = new Waiter();
                string[] values = line.Split(';');
                waiter.Id = int.TryParse(values[0], out int id) ? id : 0;
                waiter.Username = values[1];
                waiter.Password = values[2];

                _waiters.Add(waiter);
            }
            return _waiters;
        }

    }
}
