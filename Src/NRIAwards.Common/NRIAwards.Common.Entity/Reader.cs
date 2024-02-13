namespace NRIAwards.Common.Entity;

public class Reader : BaseEntity<int>
{
	public Reader(int id, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt, string name) : base(id,
		createdAt, updatedAt, deletedAt)
	{
		Name = name;
	}

	public Reader() : base()
	{
	}

	public string Name { get; set; }
}

