
namespace TimerTracker.BE.DB.Models
{
	public interface IProjectWithoutColl
	{
		string? Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
	}
}