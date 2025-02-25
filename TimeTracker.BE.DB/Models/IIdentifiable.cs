namespace TimeTracker.BE.DB.Models
{
	public interface IIdentifiable
	{
		int Id { get; }
	}

	public interface IIdentifiableGuid
	{
		Guid GuidId { get; }
	}
}