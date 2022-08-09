﻿using System.Reflection;
using Autofac;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Extentsions;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Validators.Department;

namespace TestAPI.Web;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting(options => { options.LowercaseUrls = true; });

        services.AddControllers();
        services.AddSwaggerGen();


        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Transient);
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        //вынести настройку валидаторов в отдельный метод?
        //регистрировать по коннкретному типу, или IValidator<T>?
        builder.RegisterType(typeof(CreateDepartmentCommandValidator))
            .As<IValidator<CreateDepartmentCommand>>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType(typeof(DataContext))
            .As<DataContext>()
            .InstancePerLifetimeScope();

        var domainTypes = new[]
        {
            typeof(IQuery),
            typeof(ICommand),
            typeof(IHandler)
        };

        builder.RegisterAssemblyTypes(GetType().GetTypeInfo().Assembly)
            .Where(t => t.GetTypeInfo().ImplementedInterfaces.Intersect(domainTypes).Any())
            .AsImplementedInterfaces()
            .AsSelf()
            .InstancePerLifetimeScope();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseException();

        app.UseEndpoints(builder => { builder.MapControllers(); });

        var dataContext = app.ApplicationServices.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}