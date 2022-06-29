using Amazon.SQS;
using Amazon.SQS.Model;
using AWS.Consumer;
using AWS.Consumer.Interfaces;
using AWS.Consumer.Models;
using AWS.Consumer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var app = configuration.Build();


var serviceProvider = new ServiceCollection()
    .AddSingleton<ISQSService, SQSService>()
    .AddSingleton<IConfiguration>(app)
    .AddSingleton<IRunner, Runner>()
    .BuildServiceProvider();

var runner = serviceProvider.GetService<IRunner>();
runner.IniciarLeitura();

//Commit para voltar

Console.ReadLine();




