﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Models;

namespace RestoranoSistema.Repositories
{
    public interface ITableRepository
    {
        string[] LoadTables();

        void SaveTables(Table table);

    }
}
