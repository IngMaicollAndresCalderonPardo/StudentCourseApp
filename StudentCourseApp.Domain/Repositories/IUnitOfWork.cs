﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseApp.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
