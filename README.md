# Prueba técnica de ASP.NET con Angular

Este proyecto es una prueba técnica para la administración de productos, donde es posible ver, crear, modificar y eliminar productos. Está desarrollado en ASP.NET Core 8, y Angular 18. Se utiliza una base de datos MySQL.

## Tecnologías utilizadas

- **ASP.NET Core 8**: Backend del proyecto.
- **Angular 18**: Frontend del proyecto.
- **Node.js**: Para manejar dependencias de Angular.
- **TypeScript**: Lenguaje principal del proyecto Angular.
- **C#**: Lenguaje principal del proyecto de ASP.NET.
- **MySQL**: Para almacenar los datos de usuarios y productos.

## Requisitos previos

Para compilar la aplicación es necesario contar con lo siguiente:

- [SDK de .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js (versión 18 o superior)](https://nodejs.org)
- [Angular CLI 18](https://angular.io/cli)
  ```bash
  npm install -g @angular/cli@18
  ```
- [MySQL](https://www.mysql.com/)
- Visual Studio 2022 o superior.

## Pasos para clonar, configurar y ejecutar el proyecto

### 1. Clonar el repositorio

```bash
git clone --recursive https://github.com/Taimander/PruebaTecnicaMadata
```

### 2. Configurar el backend

1. Accede a la carpeta raíz del proyecto ASP.NET.

   ```bash
   cd PruebaTecnicaMadata
   ```

2. Restaura las dependencias de .NET:

   ```bash
   dotnet restore
   ```

3. Configura la conexión a la base de datos en el archivo `appsettings.json`.

4. Ejecuta el archivo de inicialización de la base de datos que se encuentra en `docs/DB.sql`;

### 3. Configurar el frontend

1. Accede a la carpeta `ASPMVC/ClientApp`:

   ```bash
   cd ASPMVC/ClientApp
   ```

2. Instala las dependencias de Node.js:

   ```bash
   npm install
   ```

### 4. Ejecutar el proyecto

#### Opción 1: Ejecutar ambos proyectos desde Visual Studio

1. Abre la solución del proyecto en Visual Studio.
2. Selecciona el proyecto como "proyecto de inicio".
3. Presiona `F5` o ejecuta el proyecto en modo de depuración.

#### Opción 2: Ejecutar manualmente

**Ejecutar el backend:**

Desde la carpeta raíz del proyecto, ejecuta:

```bash
dotnet run
```

El backend estará disponible en `https://localhost:7223`.

**Ejecutar el frontend:**

1. Desde la carpeta `ClientApp`, realiza un build:

   ```bash
   ng build
   ```

   Esto generará los archivos del frontend en la carpeta `wwwroot`.

2. Si deseas trabajar con Angular en modo desarrollo, usa:

   ```bash
   ng serve
   ```

   Si el servidor del backend está corriendo en modo desarrollo, la aplicación web debería actualizarse automáticamente al hacer build del proyecto de Angular.

### 5. Acceso a la aplicación

- Cuando se ejecutan ambos proyectos, puedes acceder al frontend desde el navegador en `http://localhost:7223` .

## Creditos

Desarrollado por [Samuel Sánchez Tarango](https://github.com/Taimander) como prueba técnica para la empresa [Madata](https://madata.com/).

