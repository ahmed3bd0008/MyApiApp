using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace dokumen.pub_ultimate_aspnet_core_3_web_api.ModelBinder
{
public class ArrayModelBinding : IModelBinder
{
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                      if (!bindingContext.ModelMetadata.IsEnumerableType)
                      {
                          bindingContext.Result=ModelBindingResult.Failed();
                          return Task.CompletedTask;
                      }
                      var ModelProvider=bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
                        var values = bindingContext.ValueProvider.GetValue("Value");
            if (string.IsNullOrEmpty(ModelProvider))
                      {
                          bindingContext.Result=ModelBindingResult.Success(null);
                          return Task.CompletedTask;

                      }
            //           var GenericType=bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            //           var convert=TypeDescriptor.GetConverter(GenericType);
            //           var ObjectArray=ModelProvider.Split(new []{","},
            //               StringSplitOptions.RemoveEmptyEntries).
            //                          Select(x=>convert.ConvertFromString(x.Trim())).
            //                          ToArray();
                        var providedValue = bindingContext.ValueProvider .GetValue(bindingContext.ModelName) .ToString();
                        var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
                        var converter = TypeDescriptor.GetConverter(genericType);
                        var objectArray = providedValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) ;
                        var xx= objectArray.Select(x => converter.ConvertToString(x.Trim())).ToArray();
                        var GuidArray=Array.CreateInstance(genericType,xx.Length);
                        xx.CopyTo(GuidArray,0);
                        bindingContext.Model=GuidArray;
                        bindingContext.Result=ModelBindingResult.Success(bindingContext.Model);
                          return Task.CompletedTask;
            }
}
}