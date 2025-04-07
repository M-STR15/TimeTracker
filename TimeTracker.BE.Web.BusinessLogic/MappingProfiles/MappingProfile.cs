using AutoMapper;
using TimeTracker.BE.DB.Models;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.PC.Models;

namespace TimeTracker.BE.Web.BusinessLogic.MappingProfiles
{
	internal class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Project, ProjectBaseDto>().ReverseMap();
			CreateMap<Project, ProjectInsertDto>().ReverseMap();
			CreateMap<ProjectBaseDto, ProjectInsertDto>().ReverseMap();

			CreateMap<SubModule, SubModuleBaseDto>().ReverseMap();

			CreateMap<TypeShift, TypeShiftBaseDto>().ReverseMap();
			CreateMap<Shift, ShiftBaseDto>().ReverseMap();

			CreateMap<RecordActivity, RecordActivityBaseDto>().ReverseMap();
			CreateMap<RecordActivity, RecordActivityInsertDto>().ReverseMap();
			CreateMap<RecordActivity, RecordActivityDetailDto>().ReverseMap();

			CreateMap<Activity, ActivityBaseDto>().ReverseMap();

			CreateMap<TotalTimesDto, TotalTimes>().ReverseMap();
		}
	}
}
