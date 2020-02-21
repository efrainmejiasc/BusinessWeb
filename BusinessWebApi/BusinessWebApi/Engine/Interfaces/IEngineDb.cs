﻿using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine.Interfaces
{
    public interface IEngineDb
    {
       bool CreateUser(UserApi user);
        UserApi GetUser(string password);
    }
}
