
using Swashbuckle.AspNetCore.Annotations;

namespace CIoTD.Domain
{
    public class Devices
    {
        [SwaggerSchema(Description = Constants.IdentifierDeviceSwaggerSchemaDescription)]
        public string? Identifier { get; set; }
        [SwaggerSchema(Description = Constants.DescriptionDeviceSwaggerSchemaDescription)]
        public string? Description { get; set; }
        [SwaggerSchema(Description = Constants.ManufacturerDeviceSwaggerSchemaDescription)]
        public string? Manufacturer { get; set; }
        [SwaggerSchema(Description = Constants.UrlDeviceSwaggerSchemaDescription)]
        public string? Url { get; set; }
        [SwaggerSchema(Description = Constants.CommandDeviceSwaggerSchemaDescription)]
        public List<Command> Commands { get; set; }
        [SwaggerSchema(Description = Constants.RainFallDeviceSwaggerSchemaDescription)]
        public List<RainFallIntensity> RainFallIntensities { get; set; }
    }
}
