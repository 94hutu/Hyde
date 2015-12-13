﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hyde.Domain.Model;
using Hyde.Repository;
using Hyde.Api.Services;
using Hyde.Api.Models;
using Hyde.Api.Model.RequestCommands;
using Hyde.Api.Model.RequestModels;
using Hyde.Api.Filters;
using AutoMapper;
namespace Hyde.Api.Host.Controllers
{
    [InvalidModelStateFilter]
    public class SupplyController : ApiController
    {


        private readonly ISupplyService service;

        public SupplyController(ISupplyService Service)
        {
            service = Service;
        }

        [HttpGet]
        public PageResult<SupplyAdd> GetSupplyList([FromUri]PageCommand Page, string Name = null, string Code = null, Boolean? ShuOut = null)
        {
            var page = Page ?? new PageCommand();

            var result = service.GetSupplyList(page.PageIndex, page.PageSize, Name, Code, ShuOut);

            
            return new PageResult<SupplyAdd>()
            {
                PageIndex = result.PageNumber,
                PageSize = result.PageSize,
                TotalItem = result.TotalItemCount,
                ToTalPage = result.PageCount,
                Entities = result.Select(t => Mapper.Map<SupplyAdd>(t))
            };
        }
        [HttpPost]
        [EmptyParameterFilter("Item")]
        public HttpResponseMessage AddSupply(SupplyEdit Item)
        {
            var Dto = Item.Mapper();

            var result = service.AddSupply(Dto);

            if (!result.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, Dto.key);

        }

        [HttpPost]
        [EmptyParameterFilter("Item")]
        public HttpResponseMessage EditSupply(int Key, SupplyEdit Item)
        {
            var Dto = Item.Mapper();
            Dto.key = Key;
            if (!service.EditSupply(Dto).IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage DeleteSupply(int Key)
        {
            if (!service.DeleteSupply(Key).IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
