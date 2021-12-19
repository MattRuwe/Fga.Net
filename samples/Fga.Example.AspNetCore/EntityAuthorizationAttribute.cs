﻿using Fga.Net.AspNetCore.Authorization.Attributes;

namespace Fga.Example.AspNetCore;

public class EntityAuthorizationAttribute : ComputedAuthorizationAttribute
{
    private readonly string _prefix;
    private readonly string _routeValue;
    public EntityAuthorizationAttribute(string prefix, string routeValue)
    {
        _prefix = prefix;
        _routeValue = routeValue;
    }

    public override ValueTask<string> GetUser(HttpContext context)
    {
        return ValueTask.FromResult(context.User.Identity!.Name!);
    }

    public override ValueTask<string> GetRelation(HttpContext context)
    {
        return ValueTask.FromResult(context.Request.Method switch
        {
            "GET" => "viewer",
            "POST" => "writer",
            _ => "owner"
        });
    }

    public override ValueTask<string> GetObject(HttpContext context)
    {
        return ValueTask.FromResult($"{_prefix}:{context.GetRouteValue(_routeValue)}");
    }
}