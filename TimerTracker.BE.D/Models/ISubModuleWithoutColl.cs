﻿
namespace TimerTracker.BE.D.Models
{
	public interface ISubModuleWithoutColl
	{
		string? Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		int ProjectId { get; set; }
	}
}