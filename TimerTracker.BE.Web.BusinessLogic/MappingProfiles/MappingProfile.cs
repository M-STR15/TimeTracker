using AutoMapper;
using TimerTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.BE.DB.Models;

namespace TimerTracker.BE.Web.BusinessLogic.MappingProfiles
{
	internal class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Project, ProjectBaseDto>().ReverseMap();
			CreateMap<SubModule, SubModuleBaseDto>().ReverseMap();

			CreateMap<TypeShift, TypeShiftBaseDto>().ReverseMap();
			CreateMap<Shift, ShiftBaseDto>().ReverseMap();

			CreateMap<RecordActivity, RecordActivityBaseDto>().ReverseMap();
			CreateMap<RecordActivity, RecordActivityInsertDto>().ReverseMap();
			CreateMap<Activity, ActivityBaseDto>().ReverseMap();
		}
	}
}
