using DataEntities.Common;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAPI.Common
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        #region ISchemaFilter Members

        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            if (schema?.Properties == null || type == null)
                return;

            var excludedProperties = type.GetProperties()
                                         .Where(t =>
                                                t.GetCustomAttribute<SwaggerExcludeAttribute>()
                                                != null);

            foreach (var excludedProperty in excludedProperties)
            {
                if (schema.Properties.ContainsKey(excludedProperty.Name))
                    schema.Properties.Remove(excludedProperty.Name);
            }
        }

        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context.SystemType == null)
                return;

            var excludedProperties = context.SystemType.GetProperties()
                                         .Where(t =>
                                                t.GetCustomAttribute<SwaggerExcludeAttribute>()
                                                != null);

            foreach (var excludedProperty in excludedProperties)
            {
                if (schema.Properties.ContainsKey(excludedProperty.Name.ToCamelCase()))
                    schema.Properties.Remove(excludedProperty.Name.ToCamelCase());
            }
        }

        #endregion
    }
}
