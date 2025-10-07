using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace TimeTracker.Web.Blazor.Server.Helpers
{
	public class EnumSchemaFilter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (!context.Type.IsEnum)
				return;

			var enumDescriptions = Enum.GetValues(context.Type)
				.Cast<Enum>()
				.Select(value =>
				{
					var description = value.GetType()
						.GetField(value.ToString())
						?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();

					var intValue = Convert.ToInt32(value);
					return $"{intValue} = {value} ({description})";
				});

			schema.Description += "<br><b>Values:</b><br>" + string.Join("<br>", enumDescriptions);
		}
	}
}
