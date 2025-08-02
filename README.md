autor: Jeremías Herminio de Leon Godinez, jerededeleon@yahoo.com
<img width="1679" height="845" alt="Captura de pantalla 2025-08-02 a la(s) 13 50 11" src="https://github.com/user-attachments/assets/16b4c122-943a-489c-9c7d-18d95d0ac0ae" />

# 📊 Evaluación .NET/C\# Jr - Control de Ventas

Este proyecto fue desarrollado como solución al ejercicio propuesto en la evaluación técnica para desarrolladores junior en .NET/C\#, publicada en GitLab bajo el proyecto `Evaluaciones_GD`.

Autor:   Jeremías Herminio de León Godínez  

-----

## ✅ Cumplimiento de requisitos

### A) Base de Datos

✔️ Modelo entidad-relación implementado según referencia proporcionada.  
✔️ Consulta que obtiene el   nombre de la categoría   del producto de la   última venta realizada  , según la fecha.

### B) Programación (.NET/C\# con ASP.NET)

✔️ Página web que muestra el   listado de productos vendidos  .  
✔️ Filtro por   categoría  , limitado a aquellas con ventas en el   año 2019  .  
✔️ Por defecto   no se muestran registros  .  
✔️ Al seleccionar una categoría válida, se muestran los   productos vendidos en 2019   de dicha categoría.

-----

## 🧰 Tecnologías utilizadas

  - ASP.NET Core MVC (.NET 7)
  - C\# con Entity Framework Core
  - SQL Server Express (local)
  - Visual Studio 2022
  - Bootstrap CSS (básico)

-----

## 🗃️ Restaurar la base de datos desde un archivo `.bak`

### 🔁 Instrucciones en SQL Server Management Studio (SSMS)

1.    Abrir SSMS    
       Inicia SQL Server Management Studio y conéctate a tu instancia local:  
       - `.\SQLEXPRESS` o  
       - `(localdb)\MSSQLLocalDB`

2.    Abrir asistente de restauración    
       En el Explorador de objetos, haz clic derecho sobre `Databases` (Bases de datos) y selecciona:  
       Restore Database...

3.  Seleccionar el archivo de respaldo (`.bak`)
       - En el cuadro de diálogo, selecciona   Source: Device  
       - Haz clic en el botón de tres puntos `[...]`
       - En la ventana emergente, presiona   Add...   y selecciona el archivo `.bak` proporcionado
       - Presiona   OK  

4.    Configurar detalles de restauración  
       - El asistente detectará el nombre de la base de datos desde el respaldo
       - Puedes cambiar el nombre si deseas
       - Opcionalmente, en la pestaña   Files   puedes ajustar la ubicación de los archivos `.mdf` y `.ldf`

5.    Sobrescribir base existente (opcional)  
       - Si ya existe una base con el mismo nombre, ve a la pestaña   Options   y marca:  
         ✅   Overwrite the existing database (WITH REPLACE)  

6.    Ejecutar restauración  
       - Presiona   OK   para iniciar el proceso
       - Verás un mensaje de confirmación si todo ha salido correctamente

### 📝 Notas importantes

  - Asegúrate de que el archivo `.bak` esté en una carpeta accesible como `C:\Backups\`  
  - Evita carpetas como Escritorio, Documentos o Descargas  
  - Si hay errores de permisos, asegúrate de que el servicio de SQL Server tenga acceso al archivo

-----

## ⚙️ Configuración del archivo `appsettings.json`

Una vez restaurada la base de datos, asegúrate de que la cadena de conexión esté configurada correctamente en `appsettings.json`:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ControlVentasDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  },
  "AllowedHosts": " "
}
