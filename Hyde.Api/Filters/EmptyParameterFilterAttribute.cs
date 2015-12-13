﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Collections;
namespace Hyde.Api.Filters

{
    /// <summary>
    /// 筛选器,验证模型参数是否为空
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class EmptyParameterFilterAttribute : ActionFilterAttribute
    {
        public string ParameterName { get; set; }

        public EmptyParameterFilterAttribute(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentNullException(parameterName);
            ParameterName = parameterName;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            object parameterValue;
            if (actionContext.ActionArguments.TryGetValue(ParameterName, out parameterValue))
            {
                if (parameterValue == null)
                {
                    actionContext.ModelState.AddModelError(
                        ParameterName, FormatErrorMessage(ParameterName));
                    var result = actionContext.ModelState.Values.SelectMany(t => t.Errors, (p, t) => t.ErrorMessage);
                    actionContext.Response = actionContext
                        .Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }

            }
        }

        private string FormatErrorMessage(string parameterName)
        {
            return string.Format("参数{0}不能为null", parameterName);
        }
    }
}