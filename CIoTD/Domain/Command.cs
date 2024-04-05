
using Swashbuckle.AspNetCore.Annotations;

namespace CIoTD.Domain
{
    public class Command
    {
        [SwaggerSchema(Description = Constants.CommandCommandSwaggerSchemaDescription)]
        public string? Comand { get; set; }
        [SwaggerSchema(Description = Constants.ParametersCommandSwaggerSchemaDescription)]
        public List<Parameter> Parameters { get; set; }
    }    
}