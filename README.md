# Punto de Venta

Sistema de punto de venta con tres partes que comparten una misma API:

| Proyecto | Qué es | Para quién |
| --- | --- | --- |
| `MiApiTP` | API REST (.NET 10, EF Core, PostgreSQL) | Es el backend de las otras dos |
| `MiApiTP.Web` | Blazor WebAssembly (Stand Alone) | Administrador y cajero, en el mostrador |
| `PuntoDeVenta.Movil` | Blazor Hybrid (.NET MAUI) para Android | Clientes (tienda online) y administrador |

Trabajos prácticos: **TP3 Programación Web** y **TP6 Programación Móvil** (ITES).

## En línea

- API: https://miapitp.runasp.net
- Web: https://miapitp-web.runasp.net

Alojados en MonsterASP (plan gratuito), con HTTPS y certificado SSL. La base de datos es PostgreSQL en el mismo hosting. La app móvil se instala en Android y usa la API publicada.

## Qué puede hacer cada rol

| Rol | Dónde | Qué hace |
| --- | --- | --- |
| Administrador | Web y móvil | Gestiona productos, categorías, proveedores y clientes; registra ingresos de mercadería; vende; ve ventas, reportes PDF, auditoría y usuarios |
| Cajero | Web | Registra ventas (elige cliente y productos, descuenta stock y obtiene el ticket) |
| Cliente | Móvil | Se registra, mira el catálogo, arma el carrito, paga (simulado) y consulta sus compras |
| Público | Móvil | Ve el catálogo sin iniciar sesión |

## Funcionalidades principales

- ABM de productos (con imagen), categorías, proveedores, clientes y usuarios.
- Ingreso de mercadería: suma stock y registra proveedor y responsable.
- Venta con varios productos: valida el stock, guarda el precio de cada línea y genera un ticket.
- Tienda online en el móvil: catálogo en tarjetas, carrito, pago simulado y mis compras.
- Reportes en PDF (stock, entradas y salidas) para consulta.
- Control de acceso por roles con JWT.
- Auditoría de acciones y registro de errores (Serilog).

## Tecnologías

- .NET 10, ASP.NET Core Web API, Entity Framework Core con Npgsql (PostgreSQL).
- JWT (autenticación), BCrypt (hash de contraseñas), Serilog (logs), QuestPDF (reportes).
- Blazor WebAssembly con MudBlazor y Bootstrap.
- .NET MAUI Blazor Hybrid con MudBlazor.

## Arquitectura de la API

```
Controllers  →  Servicios  →  Repositorios  →  Data (DbContext)  →  PostgreSQL
     ↑
    DTOs (lo que viaja entre la API y las apps)
```

- **Controllers:** reciben los pedidos, controlan el rol (`[Authorize]`) y llaman a los servicios.
- **Servicios:** reglas de negocio (stock, validaciones, ventas, ingresos, tienda).
- **Repositorios y Data:** acceso a la base con EF Core. Las migraciones se aplican solas al iniciar.
- **DTOs:** cada pantalla recibe solo los datos que necesita.

## Seguridad

1. `POST api/Usuario/Login` verifica la contraseña (BCrypt) y devuelve un token JWT con el id, el email y el rol, válido por 2 horas.
2. La app guarda el token (`localStorage` en la Web, `SecureStorage` en el móvil) y lo envía en cada pedido en el encabezado `Authorization: Bearer <token>`.
3. La API valida la firma y el vencimiento, y `[Authorize(Roles = "...")]` decide quién entra a cada endpoint.
4. Las apps también ocultan pantallas según el rol, pero la protección real está en la API.
5. El registro público del móvil crea siempre usuarios con rol `Cliente`.

## Auditoría y logs

- **Auditoría:** cada acción importante (ventas, ingresos, altas, compras online) se guarda con usuario, entidad, detalle, fecha e IP. El administrador la consulta en la pantalla **Auditoría** de la Web.
- **Errores:** Serilog registra los eventos y errores de la API en archivos de log.

## Cómo ejecutarlo en local

Requisitos: .NET 10 SDK, PostgreSQL y Visual Studio 2022 o superior (con las cargas de ASP.NET y de .NET MAUI).

1. **API (`MiApiTP`)**
   - Configurá `ConnectionStrings:DefaultConnection` en `appsettings.Development.json` con los datos de tu PostgreSQL.
   - Configurá `Jwt:Key` (una clave larga y secreta).
   - Ejecutá el proyecto: crea y actualiza la base automáticamente con las migraciones.
2. **Web (`MiApiTP.Web`)**
   - En `wwwroot/appsettings.json`, poné en `ApiUrl` la dirección de la API.
   - Ejecutá el proyecto.
3. **Móvil (`PuntoDeVenta.Movil`)**
   - En `MauiProgram.cs`, ajustá la dirección base del cliente HTTP (por defecto apunta a la API publicada).
   - Elegí un emulador Android (o un teléfono conectado) y ejecutá el proyecto.


## Base de datos

Las tablas principales son `Usuario`, `Cliente`, `Proveedor`, `CategoriaProducto`, `Producto`, la cabecera, las líneas de cada ingreso y de cada venta, y `Auditoria`. La venta se divide en cabecera y detalle, y cada entidad tiene su tabla con claves foráneas. `Cliente.UsuarioId` es opcional y vincula a un cliente con su usuario de la tienda online.

## Despliegue

- API y Web en MonsterASP, publicadas con el perfil de publicación de Visual Studio. Al publicar la API no se tilda "borrar archivos adicionales", porque borraría la carpeta `uploads` con las imágenes.
- La configuración real se lee de `appsettings.Production.json` en el servidor.
- El certificado HTTPS se genera y activa desde el panel del hosting.

**Web**
- Login
- Venta y ticket
- Ingreso de mercadería
- Reportes y auditoría

**Móvil**
- Catálogo
- Carrito y pago
- Mis compras
- Ingresos y reportes (administrador)
