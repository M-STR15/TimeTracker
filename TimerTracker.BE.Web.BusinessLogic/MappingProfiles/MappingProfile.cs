using AutoMapper;
using TimeTracker.BE.DB.Models;

namespace TimerTracker.BE.Web.BusinessLogic.MappingProfiles
{
	internal class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Project, ProjectBaseDto>().ReverseMap();
		}
	}
}
