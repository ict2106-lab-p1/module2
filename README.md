## Getting started
- Have Node >v16
- Run `npm install` 

## Running
- `npm run dev`
- Then, in a separate window: `dotnet watch --project LivingLab.Web`

## For Production
- `npm run prod`

## Format
`dotnet format --severity warn`

## Migrations 
**skip this part if no changes to db.  
`dotnet ef migrations add CreateInitialDB -s LivingLab.Web -p LivingLab.Infrastructure`

## DB
`dotnet ef database update -s LivingLab.Web -p LivingLab.Infrastructure`

## Structure
Items that belongs to each layer:
### Core/Domain Layer (LivingLab.Core)
- Entities (business model classes that are persisted)
- Aggregates (groups of entities)
- Interfaces
- Domain Services
- Specifications
- Custom Exceptions and Guard Clauses
- Domain Events and Handlers

### Infrastructure Layer (LivingLab.Infrastructure)
- EF Core types (DbContext, Migration)
- Data access implementation types (Repositories)
- Infrastructure-specific services (for example, FileLogger or SmtpNotifier)

### Presentation Layer (LivingLab.Web)
- View Models
- Api Models
- Binding Models
- Controllers
- Api Controllers
- Views
- Razor Pages
- Razor Components
- Identity
- Authorization
- Swagger 

### References
- [Common Web Application Architectures](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [Clean Architecture with .NET and .NET Core](https://medium.com/dotnet-hub/clean-architecture-with-dotnet-and-dotnet-core-aspnetcore-overview-introduction-getting-started-ec922e53bb97#:~:text=With%20Clean%20Architecture%2C%20the%20Domain,different%20kinds%20of%20business%20logic.)
- [Github Repo: Clean Architecture by Ardalis (mostly reference from here)](https://github.com/ardalis/CleanArchitecture)
- [Github Repo: Onion Architecture by Amitpnk](https://github.com/Amitpnk/Onion-architecture-ASP.NET-Core)
- [Github Repo: Clean Architecture by blazorhero](https://github.com/blazorhero/CleanArchitecture)

## FAQ
### Tailwind
An npm script has been set up with the appropriate calls to the tailwindcli (see `package.json`). Newer versions of tailwind operate in JIT mode. The tl;dr here is your css (`webroot/css/index.css`) needs to be *compiled* before it can be *included*. 

It's output will be stored at `webroot/dist/site.css`, where it will be referenced by the main layout file (`Views/Shared/_Layout.cshtml`).

#### dev
Running `npm run dev` will start tailwindcli in "watch" mode. Any changes you make will automatically be reflected.

#### prod
Running `npm run dev` will start tailwindcli in build mode. In addition to compiling your `index.css`, the resulting output will be minified as well.

### Nullable warnings
It's "safe" to ignore warnings on startup about nullables. These are scaffolded from a template. Feel free to correct them. See https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references.

### Database
Install ef tools if they aren't already present.
`dotnet tool install --global dotnet-ef`

#### Run migrations
- `dotnet ef database update -s LivingLab.Web -p LivingLab.Infrastructure`

Alternatively, run the web app and let dotnet run migrations automatically. If you encounter an exception page, click on "Run migrations" and refresh.

#### Clean state
Remove `livinglab.sqlite`, rerun migrations.

---

Credits: Thanks Percy for setting up the MVC skeleton + Tailwind integration.