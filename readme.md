
---

```markdown
# Eternal API

API desarrollada con **.NET 9**, siguiendo principios de **Clean Architecture** y **Vertical Slice Architecture (VSA)**.  
La API expone endpoints RESTful para manejar entidades del dominio (ejemplo: `Bill`), conectada a **PostgreSQL** como base de datos.

---

## 📂 Estructura del Proyecto

```

eternal-api/
│── eternal\_api.Domain/              # Entidades de dominio (reglas de negocio puras)
│── eternal\_api.Application/         # Casos de uso (Handlers, Commands, Queries)
│── eternal\_api.Infraestructure/     # Persistencia, repositorios, EF Core
│── eternal\_api.WebAPI/              # API (endpoints, Program.cs, Swagger)

````

---

## 🛠️ Tecnologías

- **.NET 9**
- **Entity Framework Core** (con PostgreSQL provider `Npgsql`)
- **Vertical Slice Architecture (VSA)**
- **Swagger / Swashbuckle**
- **Dependency Injection (DI)** integrada en ASP.NET Core

---

## 🗄️ Base de Datos

El proyecto usa **PostgreSQL 14+**.

### 🔑 Configuración de la contraseña

En el archivo `appsettings.json` la conexión se define así:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=eternal_db;Username=postgres;Password={DB_PASSWORD}"
  }
}
````

* Observa que el campo `Password` contiene `{DB_PASSWORD}` como **placeholder**.
* Ese valor se reemplaza en tiempo de ejecución con una **variable de entorno**.

### 📌 Cómo definir la variable de entorno `DB_PASSWORD`

En **Windows (PowerShell)**:

```powershell
setx DB_PASSWORD "tu_password_aqui"
```

En **Linux/Mac**:

```bash
export DB_PASSWORD="tu_password_aqui"
```

Una vez definida, al ejecutar la API el `Program.cs` reemplazará `{DB_PASSWORD}` por el valor real y la conexión funcionará.

---

## 🚀 Configuración del `Program.cs`

El `Program.cs` se encarga de:

1. Leer la **cadena de conexión**.
2. Reemplazar el `{DB_PASSWORD}` con la variable de entorno.
3. Configurar **DbContext** con EF Core + PostgreSQL.
4. Inyectar **repositorios** y **handlers**.
5. Configurar **Swagger** y mapear endpoints.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Leer ConnectionString
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Reemplazar la contraseña con variable de entorno
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
if (string.IsNullOrEmpty(password))
{
    throw new Exception("La variable de entorno DB_PASSWORD no está definida.");
}
connString = connString.Replace("{DB_PASSWORD}", password);

// Registrar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));

// Inyección de dependencias
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<CreateBillHandler>();
builder.Services.AddScoped<GetBillByIdHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Mapear endpoints personalizados
app.MapBillEndpoints();

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

## 🧩 Inyección de Dependencias

ASP.NET Core usa **Dependency Injection (DI)** por defecto.
Cada vez que una clase tenga dependencias, deben registrarse en `builder.Services`.

Ejemplo en este proyecto:

* **Repositorio de Bills**

  ```csharp
  builder.Services.AddScoped<IBillRepository, BillRepository>();
  ```

* **Handlers de aplicación**

  ```csharp
  builder.Services.AddScoped<CreateBillHandler>();
  builder.Services.AddScoped<GetBillByIdHandler>();
  ```

⚠️ **Regla general:**
Todo lo que implemente una **interfaz** debe ser registrado en `builder.Services`.
Si no lo haces, obtendrás un error del tipo:
`System.InvalidOperationException: Unable to resolve service for type ...`

---

## 📜 Migraciones con EF Core

1. Crear una migración:

   ```bash
   dotnet ef migrations add InitialCreate --project eternal_api.Infraestructure --startup-project eternal_api.WebAPI
   ```

2. Aplicar la migración:

   ```bash
   dotnet ef database update --project eternal_api.Infraestructure --startup-project eternal_api.WebAPI
   ```

3. Si necesitas revertir la última migración:

   ```bash
   dotnet ef migrations remove --project eternal_api.Infraestructure --startup-project eternal_api.WebAPI
   ```

---

## 📌 Endpoints disponibles

Ejemplo con **Bills**:

* `POST /bills` → Crea una nueva factura (`CreateBillHandler`).
* `GET /bills/{id}` → Obtiene una factura por ID (`GetBillByIdHandler`).

Estos endpoints se definen en `WebAPI/Endpoints/BillEndpoints.cs` y se registran con:

```csharp
app.MapBillEndpoints();
```

---

## 🧪 Swagger

Para probar la API, ejecuta el proyecto y abre:

```
https://localhost:5001/swagger
```

---

## 👨‍💻 Buenas Prácticas

* Usa **GUID** como `Id` en todas las entidades para consistencia.
* Documenta cualquier nuevo endpoint en este README.
* Mantén las migraciones aplicadas y sincronizadas.
* Nunca pongas contraseñas en `appsettings.json`, usa **variables de entorno**.
* Si agregas un nuevo **repositorio o handler**, recuerda **inyectarlo en `Program.cs`**.

---

```

---
