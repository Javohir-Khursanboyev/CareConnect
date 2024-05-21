﻿using CareConnect.Service.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class CustomAuthorize : Attribute, IAuthorizationFilter
{
   // private readonly IRolePermissionService rolePermissionService;
    public CustomAuthorize()
    {
       // rolePermissionService = InjectHelper.RolePermissionService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var actionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

        var allowAnonymous = actionDescriptor?.MethodInfo.GetCustomAttributes(inherit: true)
                .OfType<AllowAnonymousAttribute>().Any() ?? false;
        if (allowAnonymous) return;

        string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            SetStatusCodeResult(context);
            return;
        }

        var action = actionDescriptor.ActionName;
        var controller = actionDescriptor.ControllerName;
        var role = context.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        //if (//!rolePermissionService.CheckRolePermission(role, action, controller))
        //{
        //    SetStatusCodeResult(context);
        //    return;
        //}
    }

    private void SetStatusCodeResult(AuthorizationFilterContext context)
    {
        var exception = new CustomException(403, "You do not have permission for this method");
        context.Result = new ObjectResult(new Response
        {
            StatusCode = exception.StatusCode,
            Message = exception.Message
        })
        {
            StatusCode = exception.StatusCode
        };
    }
}