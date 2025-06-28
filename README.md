# StudentCourseApp

Aplicación web ASP.NET Core MVC para la gestión de estudiantes y materias inscritas, desarrollada como parte de una prueba técnica. Implementa arquitectura limpia y principios SOLID, usando .NET 8 y Entity Framework Core 9.

---

## 🚀 Características

- ✅ Crear, editar, eliminar y listar estudiantes
- ✅ Crear, editar, eliminar y listar materias
- ✅ Inscribir materias a estudiantes
- ✅ Validaciones de negocio:
  - Máximo 3 materias con más de 4 créditos
  - No se puede inscribir la misma materia dos veces
- ✅ Modal de confirmación para eliminar
- ✅ Alertas de éxito y error
- ✅ Buscador en la lista
- ✅ Pruebas unitarias

---

## 🛠️ Tecnologías utilizadas

- .NET 8
- ASP.NET Core MVC con Razor Pages
- Entity Framework Core 9
- SQLite (por defecto usando `studentcourse.db`, configurable desde `appsettings.json`)
- Bootstrap 5
- Pruebas con xUnit

---

## 📁 Estructura del proyecto
StudentCourseApp.sln
│
├── StudentCourseApp.Domain # Entidades y reglas de negocio
├── StudentCourseApp.Application # Interfaces y servicios
├── StudentCourseApp.Infrastructure # EF Core, repositorios, DBContext
├── StudentCourseApp # ASP.NET Core MVC (UI)
└── StudentCourseApp.Tests # Pruebas unitarias

## ▶️ ¿Cómo ejecutar?

### 1. Clonar el repositorio
### 2. Restaurar paquetes y compilar
### 3. crear base de datos
### 4. Asegúrate de tener instalada la herramienta de EF
### 5. Ejecutar la aplicación

💡 Nota: Esta aplicación usa SQLite como base de datos local. El archivo `studentcourse.db` se generará automáticamente en la raíz del proyecto cuando se aplique la migración.

```bash
git clone https://github.com/IngMaicollAndresCalderonPardo/StudentCourseApp.git
cd StudentCourseApp
dotnet restore
dotnet build
dotnet tool install --global dotnet-ef
dotnet ef database update --project StudentCourseApp
dotnet run --project StudentCourseApp
```
Para ejecutar las pruebas unitarias:
```bash
dotnet test
```

🙋 Autor
Maicoll Andrés Calderón Pardo
📧 ing.maicollcalderonpardo@gmail.com
🔗 GitHub - @IngMaicollAndresCalderonPardo


