# Configuración del Proyecto

## Requisitos previos:
- Visual Studio 2022
- .NET 7

## Instalación:
1. **Módulos de Node**: Navega al directorio `react-front` e instala los módulos de node necesarios:
    ```
    npm i
    ```

2. **Configuración del entorno**: Asegúrate de que el archivo `.env` esté configurado para apuntar al entorno local.

## Ejecución del proyecto .NET Core (`net7-core-vs2022`):
- No es necesario ejecutar las migraciones ya que ya apunta a una base de datos en producción.

## APIs:
Para las APIs disponibles, consulta la documentación de Swagger:
[Swagger Local](https://localhost:7237/swagger/index.html)

## Creación de usuarios:
- Registra un nuevo usuario a través de la interfaz de Swagger usando el endpoint 'register'.
- Después de registrarte, puedes iniciar sesión para obtener un token JWT.
- Sin embargo, con solo registrarte podrás acceder al frontend en [http://localhost:3000](http://localhost:3000).

## Funcionalidades próximas:
- Estoy trabajando en implementar formularios dinámicos. Mientras tanto, puedes revisar la estructura y el esquema de la base de datos.
