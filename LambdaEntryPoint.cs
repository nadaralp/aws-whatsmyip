﻿using Microsoft.AspNetCore.Hosting;

namespace WhatsMyIp
{
    public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder) => builder.UseStartup<Startup>();
    }
}