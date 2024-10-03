# ScaffoldingSolution

## Descripción

**ANTp** es un proyecto de ejemplo que implementa la arquitectura limpia (Clean Architecture) utilizando ASP.NET Core. Este proyecto demuestra cómo estructurar una aplicación web con separación de preocupaciones, utilizando SQL Server como bases de datos.

## Estructura del Proyecto

La solución está organizada en múltiples capas y proyectos, cada uno con una responsabilidad específica:

- **ANTpApi**: Proyecto de la API ASP.NET Core. Contiene los controladores y la configuración de la aplicación.
- **Application**: Capa de aplicación. Contiene los servicios y la lógica de negocio.
- **Domain**: Capa de dominio. Contiene las entidades y las interfaces de los repositorios.
- **Infrastructure**: Capa de infraestructura. Contiene las implementaciones de los repositorios y el contexto de la base de datos.

## Dependencias

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [MongoDB Driver](https://www.mongodb.com/docs/drivers/csharp/)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) (para Swagger)

## Instalación

1. Clona el repositorio:

   ```bash
   
   ```

2. Restaura las dependencias:

   ```bash
   dotnet restore
   ```

3. Configura las cadenas de conexión en `appsettings.json` en el proyecto `ANTpApi`:

   ```json
   {
       "ConnectionStrings": {
           "SqlServer": "Your SQL Server connection string"
       },
       "MongoDB": {
           "ConnectionString": "Your MongoDB connection string",
           "DatabaseName": "Your MongoDB database name"
       }
   }
   ```

## Uso

1. Compila la solución:

   ```bash
   dotnet build
   ```

2. Ejecuta la aplicación:

   ```bash
   dotnet run --project ANTpApi/ANTpApi.csproj
   ```

3. Accede a Swagger para probar la API:

   Abre tu navegador y navega a `https://localhost:5001/swagger`.

## Estructura del Código

### ANTpApi

- **Controllers**
  - `ProductController.cs`: Controlador para gestionar productos.
- **Startup.cs**: Configuración de servicios y middleware.

### Application

- **Interfaces**
  - `IProductService.cs`: Interfaz para el servicio de productos.
- **Services**
  - `ProductService.cs`: Implementación del servicio de productos.

### Domain

- **Entities**
  - `Product.cs`: Entidad de producto.
- **Interfaces**
  - `IRepository.cs`: Interfaz genérica para el repositorio.

### Infrastructure

- **Repositories**
  - `ProductRepository.cs`: Implementación del repositorio de productos.
- **Data**
  - `ApplicationDbContext.cs`: Contexto de la base de datos SQL Server.



## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o envía un pull request para discutir cualquier cambio que desees hacer.

## Licencia

Este proyecto está licenciado bajo la [MIT License](LICENSE).

---
"# clean-architecture" 
