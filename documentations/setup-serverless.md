1. Install Nuget Package
   
        <PackageReference Include="Amazon.Lambda.AspNetCoreServer" Version="6.0.2" />

2. Add to .csproj

         <AWSProjectType>Lambda</AWSProjectType>

3. Add **aws-lambda-tools-defaults.json** file to support **dotnet lambda** cli. <br />
Example config:
   
        {
        "profile":"default",
        "stack-name": "whatsmyip-serverless-stack",
        "region":"us-east-2",
        "configuration":"Release",
        "framework":"netcoreapp3.1",
        "s3-bucket":"nadar-demo-serverless",
        "template":"template.yml",
        "template-parameters":""
        }



4. Add **LambdaEntryPoint.cs** file.

        using Microsoft.AspNetCore.Hosting;

        namespace WhatsMyIp
        {
            public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
            {
                protected override void Init(IWebHostBuilder builder) => builder.UseStartup<Startup>();
            }
        }


5. Add **template.yml** file to support AWS SAM template.

        ---
        AWSTemplateFormatVersion: '2010-09-09'
        Transform: AWS::Serverless-2016-10-31
        Description: An AWS Serverless Application that uses the ASP.NET Core framework running
        in Amazon Lambda.
        Parameters: {}
        Conditions: {}
        Resources:
        AspNetCoreFunction:
        Type: AWS::Serverless::Function
        Properties:
        Handler: WhatsMyIp::WhatsMyIp.LambdaEntryPoint::FunctionHandlerAsync
        Runtime: dotnetcore3.1
        CodeUri: ''
        MemorySize: 256
        Timeout: 30
        Policies:
        - AWSLambda_FullAccess
        Environment:
        Variables: {}
        #      VpcConfig:
        #        SecurityGroupIds:
        #          - sg-0d99d5f4548646197
        #        SubnetIds:
        #          - subnet-0486b99a9e4d1be3c
              Events:
                ProxyResource:
                  Type: Api
                  Properties:
                    Path: "/{proxy+}"
                    Method: ANY
        
        Outputs:
        ApiURL:
        Description: API endpoint URL for Prod environment
        Value:
        Fn::Sub: https://${ServerlessRestApi}.execute-api.${AWS::Region}.amazonaws.com/Prod/
        


6. Run **dotnet lambda deploy-serverless**
You don't need to provide any additional configuration since it reads your **aws-lambda-tools-defaults.json** file configuration