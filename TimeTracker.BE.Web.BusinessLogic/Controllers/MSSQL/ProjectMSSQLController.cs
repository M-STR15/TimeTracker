using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.BE.DB.DataAccess;
using TimeTracker.BE.DB.Repositories;
using TimeTracker.BE.Web.Shared.Services;

namespace TimeTracker.BE.Web.BusinessLogic.Controllers.MSSQL
{
	[ApiExplorerSettings(GroupName = "v1")]
	public class ProjectMSSQLController : ProjectController<MsSqlDbContext>
	{
		public ProjectMSSQLController(ProjectRepository<MsSqlDbContext> projectRepository, SubModuleRepository<MsSqlDbContext> subModuleRepository, IMapper mapper, IEventLogService eventLogService) : base(projectRepository, subModuleRepository, mapper, eventLogService)
		{

		}
	}
}
