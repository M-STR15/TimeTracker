using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.Shared.Services;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers.InMemory
{
	[ApiExplorerSettings(GroupName = "v1")]
	public class ProjectInMemoryController : ProjectController<InMemoryDbContext>
	{
		public ProjectInMemoryController(ProjectRepository<InMemoryDbContext> projectRepository, SubModuleRepository<InMemoryDbContext> subModuleRepository, IMapper mapper, IEventLogService eventLogService) : base(projectRepository, subModuleRepository, mapper, eventLogService)
		{
		}
    }
}
