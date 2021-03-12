# Food application

![.NET Core](https://github.com/MikhailMasny/awesome-food-app/workflows/.NET%20Core/badge.svg)

A web-application developed on the .NET 5.0. The main idea of a web application is to develop a system for create a basic engine for the operation of an internet cafe. This repository can also serve as a template for creating the application with the account and some basic functionality.

## Getting Started

The developed web application is MVC (Model-View-Controller). When you start, you need to create admin and change role by database (user and administrator roles will already be added to the database). This application is a clone of the Internet cafe application for ordering food.

### SQL & EF Core migrations

For EF migrations, use the following commands from Package Manager Console:

```
update-database
```

For SQL data seed, use [this file](https://github.com/MikhailMasny/awesome-food-app/blob/main/scripts/SqlSeed.sql).

## Built with
- [ASP.NET Core 5.0](https://docs.microsoft.com/en-us/aspnet/core/);
- [N-Layer architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures);
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/);
- [Serilog](https://serilog.net/);

## Author
[Mikhail M.](https://mikhailmasny.github.io/) - Software Engineer;

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/MikhailMasny/awesome-food-app/blob/master/LICENSE) file for details.
