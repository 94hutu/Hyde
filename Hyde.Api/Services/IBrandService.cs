﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.Api.Model.RequestModels;
using Hyde.Api.Models;
using PagedList;
using Hyde.Domain.Model;
namespace Hyde.Api.Services
{
    public interface IBrandService
    {
        IPagedList<brandDto> Find(int PageIndex, int PageSize, bool? ShutOut = null);

        brandDto FindSingle(int Key);



    }
}
