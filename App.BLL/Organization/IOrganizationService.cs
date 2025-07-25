﻿using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task CreateAsync(Organization organization);
    }
}
