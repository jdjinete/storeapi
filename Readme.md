StoreApi

This is the repo for work in Api dot net core 3 and PostgreSQL with docker

1. dotnet new webapi -o StoreApi
2. dotnet build
3. dotnet run
4. Go to https://localhost:5001/WeatherForecast
5. Create Dockerfile and config csprog and dll with this information:

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY paymentdetailapi.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "paymentdetailapi.dll"]

6. Create folder Models and create the class model in this case is Product.

     public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
7. Add nuget for EntityFramework Core with PostGreSQL.
    dotnet add .\StoreApi.csproj package Npgsql.EntityFrameworkCore.PostgreSQL

8. Create folder Map and create the class Mapper of  in this case is ProductMap.

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StoreApi.Models;

        public class ProductMap
        {
            public ProductMap(EntityTypeBuilder<Product> entityBuilder)
            {
                entityBuilder.HasKey(x => x.Id);
                entityBuilder.ToTable("product");

                entityBuilder.Property(x => x.Id).HasColumnName("id");
                entityBuilder.Property(x => x.Title).HasColumnName("title");
                entityBuilder.Property(x => x.Description).HasColumnName("description");
                entityBuilder.Property(x => x.Price).HasColumnName("price");

            }
        }

9. Create class ApiDbContext in folder Models.

    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ProductMap(modelBuilder.Entity<Product>());
        }

    }
10. Create Folder Scripts and create .SQL with Script for DB.

    \connect storedb
    CREATE TABLE product
    (
    id serial PRIMARY KEY,
    title VARCHAR (50) NOT NULL,
    description VARCHAR (100) NOT NULL,
    price INT NOT NULL
    );
    ALTER TABLE "product" OWNER TO storedb;
    Insert into product(title,description,price) values( 'Papas Margarita de Limón','Paquete de papas de la marca margarita',1500);
    Insert into product(title,description,price) values( 'Ponqué Gala','Description 2',1700);
    Insert into product(title,description,price) values( 'Papas Margarita de Pollo','Description 3',1500);
    Insert into product(title,description,price) values( 'DeTodito BBQ','Description 4',2000);

11. Add docker compose

    version: '3.4'
    
    networks:
    storeapi-dev:
        driver: bridge 
    
    services:
    storeapi:
        image: storeapi:latest
        depends_on:
        - "postgres_image"
        build:
        context: .
        dockerfile: Dockerfile
        ports:
        - "5000:80"     
        environment:
        DB_CONNECTION_STRING: "host=postgres_image;port=5432;database=storedb;username=storeadmin;password=storeadmin"
        networks:
        - storeapi-dev  
    
    postgres_image:
        image: postgres:latest
        ports:
        - "5432"
        restart: always
        volumes:
        - db_volume:/var/lib/postgresql/data
        - ./Scripts/seed.sql :/docker-entrypoint-initdb.d/seed.sql
        environment:
        POSTGRES_USER: "storeadmin"
        POSTGRES_PASSWORD: "storeadmin"
        POSTGRES_DB: "storedb"
        networks:
        - storeapi-dev
    volumes:
    db_volume:

12. Configure services and ConectionString Startup.
    12.1. ConectionString and configure contextDB:
     var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<ApiDbContext>(options =>
                                                options.UseNpgsql(connectionString)
                                                );
    12.2. Add using Microsoft.EntityFrameworkCore;

13. Add controller Product.
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using StoreApi.Models;

    namespace RegaloParaTiAPI.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class ProductController : ControllerBase
        {
            private  readonly  ApiDbContext _context;

            public ProductController(ApiDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public object Get()
            {
                return _context.Products.ToList(); 
                
            }


        }


    }
14. dotnet clean
15. dotnet build
16.  add file .dockerignore dotnet core
    # directories
    **/bin/
    **/obj/
    **/out/

    # files
    Dockerfile*
    **/*.trx
    **/*.md
    **/*.ps1
    **/*.cmd
    **/*.sh
17. docker-compose up -d

    
    







