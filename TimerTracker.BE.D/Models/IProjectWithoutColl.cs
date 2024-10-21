
namespace TimerTracker.BE.D.Models
{
	public interface IProjectWithoutColl
	{
		string? Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
	}
}