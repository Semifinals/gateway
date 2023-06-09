﻿using Semifinals.Api.Utils.Authentication;
using Semifinals.Utils.GatewayFramework;
using Semifinals.Utils.GatewayFramework.Http;

namespace Semifinals.Api.Triggers;

public static class BlogController
{
    public static readonly string BlogServiceFunctionKey = Environment.GetEnvironmentVariable("BlogServiceFunctionKey")!;
    
    [FunctionName("GetBlog")]
    public static async Task<HttpResponseMessage> GetBlog(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "blogs/{slug:regex(^(?!(filter)$).*$)}")] HttpRequest req,
        string slug)
    {
        return await Flow.Handle(
            (await Request.FromHttp(req))
                .Redirect($"https://blog.api.semifinals.co/{slug}")
                .AddHeader("x-functions-key", BlogServiceFunctionKey),
            flow => flow
                .Pipe(new Passthrough())
                .Response());
    }

    [FunctionName("GetBlogsFiltered")]
    public static async Task<HttpResponseMessage> GetBlogsFiltered(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "blogs/filter")] HttpRequest req)
    {
        return await Flow.Handle(
            (await Request.FromHttp(req))
                .Redirect($"https://blog.api.semifinals.co/filter{req.QueryString}")
                .AddHeader("x-functions-key", BlogServiceFunctionKey),
            flow => flow
                .Pipe(new Passthrough())
                .Response());
    }

    [FunctionName("CreateBlog")]
    public static async Task<HttpResponseMessage> CreateBlog(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "blogs")] HttpRequest req)
    {
        return await Flow.Handle(
            (await Request.FromHttp(req))
                .Redirect("https://blog.api.semifinals.co/")
                .AddHeader("x-functions-key", BlogServiceFunctionKey),
            flow => flow
                .Pipe(new SemifinalsAuthenticator(req, true))
                .Pipe(new Passthrough())
                .Response());
    }

    [FunctionName("DeleteBlog")]
    public static async Task<HttpResponseMessage> DeleteBlog(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "blogs/{id:int}")] HttpRequest req,
        int id)
    {
        return await Flow.Handle(
            (await Request.FromHttp(req))
                .Redirect($"https://blog.api.semifinals.co/{id}")
                .AddHeader("x-functions-key", BlogServiceFunctionKey),
            flow => flow
                .Pipe(new SemifinalsAuthenticator(req, true))
                .Pipe(new Passthrough())
                .Response());
    }
}
