﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Context.Stores
{
    public class Store : IStore
    {
        public User CurrentUser { get; set; }
    }
}
