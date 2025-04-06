using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers
{
	[ApiController]
	[SwaggerResponse(200, "Úspěšné získání položky/položek [Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200)")]
	[SwaggerResponse(404, "Položka/Položky nenalezeny.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404)")]
	[SwaggerResponse(500, "Chyba serveru.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500)")]
	public abstract class aControllerBase : ControllerBase
	{
		protected readonly IMapper _mapper;

		public aControllerBase(IMapper mapper)
		{
			_mapper = mapper;
		}
	}
}
