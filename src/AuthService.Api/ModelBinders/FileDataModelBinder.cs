using System;
using System.Threading.Tasks;
using AuthService.Api.Models;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthService.Api.ModelBinders;

/// <summary>
/// FileDataModelBinder personalizado para ASP.NET Core.
/// Convierte automáticamente archivos HTTP en objetos que implementen IFileData.
/// Se utiliza para el binding automático de archivos en los parámetros de los controladores.
/// </summary>
public class FileDataModelBinder : IModelBinder
{
    /// <summary>
    /// Método que realiza el binding del archivo a un modelo.
    /// Se ejecuta cuando ASP.NET Core necesita enlazar un archivo a un parámetro de acción.
    /// </summary>
    /// <param name="bindingContext">Contexto del binding que contiene información sobre la solicitud y el modelo a enlazar</param>
    /// <returns>Tarea completada una vez que se procesa el archivo</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // Validar que el contexto de binding no sea nulo
        ArgumentNullException.ThrowIfNull(bindingContext);

        // Verificar que el tipo del modelo sea compatible con IFileData
        // Si no es compatible, terminar el proceso de binding
        if (typeof(IFileData).IsAssignableFrom(bindingContext.ModelType))
        {
            return Task.CompletedTask;
        }

        // Obtener la solicitud HTTP actual
        var request = bindingContext.HttpContext.Request;

        // Buscar el archivo en la colección de archivos de la solicitud por nombre de campo
        var file = request.Form.Files.GetFile(bindingContext.FieldName);

        // Verificar si el archivo existe y tiene contenido
        if (file != null && file.Length > 0)
        {
            // Si hay archivo válido, envolverlo en FormFileAdapter (adaptador que implementa IFileData)
            var fileData = new FormFileAdapter(file);
            // Establecer el resultado exitoso con el archivo adaptado
            bindingContext.Result = ModelBindingResult.Success(fileData);
        }
        else
        {
            // Si no hay archivo, establecer el resultado como nulo (parámetro opcional)
            bindingContext.Result = ModelBindingResult.Success(null);
        }

        // Retornar tarea completada
        return Task.CompletedTask;
    }
}

/// <summary>asdfasdfasdf
/// Proveedor de ModelBinder para FileDataModelBinder.
/// Le indica a ASP.NET Core cuándo debe utilizar FileDataModelBinder.
/// Este proveedor se registra en la configuración de servicios para detectar automáticamente
/// cuándo se necesita enlazar un archivo a un parámetro que implemente IFileData.
/// </summary>
public class FileDataModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Método que retorna el binder apropiado si el modelo implementa IFileData.
    /// </summary>
    /// <param name="context">Contexto del proveedor con información sobre el tipo de modelo a enlazar</param>
    /// <returns>Instancia de FileDataModelBinder si el tipo implementa IFileData, null en caso contrario</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        // Verificar si el tipo de modelo implementa la interfaz IFileData
        if (typeof(IFileData).IsAssignableFrom(context.Metadata.ModelType))
        {
            // Si implementa IFileData, retornar una nueva instancia del binder
            return new FileDataModelBinder();
        }

        // Si no implementa IFileData, retornar null (otro proveedor se encargará)
        return null;
    }
}