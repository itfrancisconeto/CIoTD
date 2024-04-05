using Swashbuckle.AspNetCore.Annotations;

namespace CIoTD.Domain
{
    public class RainFallIntensity
    {
        [SwaggerSchema(Description = Constants.DateTimeRainFallIntensitySwaggerSchemaDescription)]
        public DateTime? DateTime { get; set; }
        [SwaggerSchema(Description = Constants.VolumetryRainFallIntensitySwaggerSchemaDescription)]
        public double Volumetry { get; set; }
    }
}
