﻿using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Application.Services;
public interface IApplicationUserService
{
    Task<ApplicationUser?> GetById(string id);
}
