﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInterfaces
{
    public interface IAuthentificator
    {
        bool CheckUserIdentity(string userName, string password);

    }
}
