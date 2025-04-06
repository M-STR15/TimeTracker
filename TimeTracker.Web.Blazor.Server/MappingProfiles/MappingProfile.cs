using AutoMapper;
using TimeTracker.BE.Web.BusinessLogic.Models.DTOs;
using TimeTracker.Web.Blazor.Server.Models;

namespace TimeTracker.Web.Blazor.Server.MappingProfiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<RecordActivityDetailDto, RecordListViewModel>()
					.ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.Activity.Name))
					.ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
					.ForMember(dest => dest.SubModuleName, opt => opt.MapFrom(src => src.SubModule.Name))
					.ForMember(dest => dest.TypeShiftName, opt => opt.MapFrom(src => src.TypeShift.Name));
		}
	}
}
