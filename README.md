# BOOKLEND - API

## Descripción
Esta aplicación es una implementación de la arquitectura limpia (Clean Architecture) en .NET Core 8. La arquitectura limpia es un enfoque de diseño de software que facilita el mantenimiento y la escalabilidad de la aplicación a largo plazo.

## Requisitos Previos
- [.NET Core 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Configuración del Proyecto
1. Clona este repositorio en tu máquina local.
2. Abre el proyecto en tu entorno de desarrollo preferido (Visual Studio, Visual Studio Code, etc.).
3. Asegúrate de tener instalado el SDK de .NET Core 8.
4. Restaura las dependencias del proyecto ejecutando `dotnet restore` en la terminal en la raíz del proyecto.

## Estructura del Proyecto
La aplicación sigue la estructura de la arquitectura limpia, que se compone principalmente de tres capas:
- **Capa de Presentación**: Contiene la interfaz de usuario de la aplicación, como las API REST o la interfaz de usuario web.
- **Capa de Dominio**: Define las reglas de negocio y lógica de la aplicación, independiente de cualquier infraestructura o detalles de implementación.
- **Capa de Infraestructura**: Contiene la implementación concreta de la lógica de acceso a datos, servicios externos, y otros detalles de implementación.

## Base de Datos
Esta aplicación utiliza SQLite como base de datos. El archivo de la base de datos se encuentra en el directorio raíz del proyecto y se llama `Pedidos.db`.

## Ejecución del Proyecto
1. Para ejecutar la aplicación, utiliza el siguiente comando en la terminal:
    ```bash
    dotnet run --project BookLendAPI/BookLendAPI.csproj
    ```
2. Una vez que la aplicación esté en ejecución, abre un navegador web y navega a la URL correspondiente (por defecto: `https://localhost:5103`).
3. para acceder a los endpoints deberas acceder atraves de Swagger, para esto debes agregar `/swagger` o en su defecto `/index.html` quedando de la siguiente forma `https://localhost:5103/swagger/index.html` o `https://localhost:7066/index.html`.