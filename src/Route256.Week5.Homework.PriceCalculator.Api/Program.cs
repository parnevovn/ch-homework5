using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Route256.Week5.Homework.PriceCalculator.Api.NamingPolicies;
using Route256.Week5.Homework.PriceCalculator.Api.ActionFilters;
using Route256.Week5.Homework.PriceCalculator.Bll.Extensions;
using Route256.Week5.Homework.PriceCalculator.Dal.Extensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
    });

services.AddEndpointsApiExplorer();

// add swagger
services.AddSwaggerGen(o =>
{
    o.CustomSchemaIds(x => x.FullName);
});

//add validation
services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = true;
});

services.AddMvc()
    .AddMvcOptions(x =>
    {
        x.Filters.Add(new ExceptionFilterAttribute());
       // x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
       // x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.BadRequest));
      //  x.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
    });

//add dependencies
services
    .AddBll()
    .AddDalInfrastructure(builder.Configuration)
    .AddDalRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MigrateUp();
app.Run();
