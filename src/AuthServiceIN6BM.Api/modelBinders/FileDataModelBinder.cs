using AuthService.Api.Models;
using AuthServiceIN6BM.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthServiceIN6BM.Api.ModelBinding;

public class FileDataModelBinder : IModelBinder
{
    public Task BinModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.TrowIfNull(bindingContext);

        if (!typeof(IFileData).IsAssignableFrom(bindingContext.ModelType))
        {
            return Task.CompletedTask;
        }

        var request = bindingContext.HttpContext.Request;

        var file = request.Form.Files.GetFile(bindingContext.FieldName);

        if (file != null && file.lenght > 0)
        {
            var fileData = new formFileAdapter(file);
            bindingContext.Result = ModelBinding.Success(fileData);
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Success(null);
        }
        return Task.CompletedTask;
    }

    public class FileDataModelBinderProvider : ImodelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (typeof(IFileData).IsAssignableFrom(context.MetaData.ModelType))
            {
                return new FileDataModelBinder();
            }
            return null;
        }
    }
}