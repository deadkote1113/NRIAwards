using Microsoft.EntityFrameworkCore;
using NRIAwards.Common.Configuration;
using NRIAwards.DAL.Context.Model;

namespace NRIAwards.DAL.Context;

public class PostgresDbContext : DbContext
{
	public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
	{
		Database.Migrate();
	}

	public DbSet<Award> Awards { get; set; }
	public DbSet<AwardEvent> AwardEvents { get; set; }
	public DbSet<AwardSession> AwardSessions { get; set; }
	public DbSet<Nomination> Nominations { get; set; }
	public DbSet<Nominee> Nominee { get; set; }
	public DbSet<Reader> Reader { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<VisualContent> VisualContents { get; set; }
	public DbSet<Vote> Votes { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>().HasData(
		new User()
		{
			Id = 1,
			Login = "admin",
			//1qw23er45ty6
			Password = "6fe7785e8523e09070fa676fa94c272b09c11699149a2a7589e67bf8ce81fd97ffb944005390f83e5eb1299383fc2b6c42bfc902e0daf106d64c3b574f68112f",
			RoleId = UserRole.Admin,
			VisualContentId = SharedConfiguration.VisualContentPlaceholderId,
			CreatedAt = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc),
			UpdatedAt = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc),
			IsBlocked = false,
		});

		modelBuilder.Entity<VisualContent>().HasData(
			new VisualContent()
			{
				Id = SharedConfiguration.VisualContentPlaceholderId,
				Title = "Placeholder",
				Type = VisualContentType.Picture,
				Link = "",
				CreatedAt = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc),
				UpdatedAt = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc),
			});
	}
}

public static class IdFactory<TEntity>
{
	private static int _counter;

	static IdFactory()
	{
		_counter = 0;
	}

	static int GetId()
	{
		_counter++;
		return _counter;
	}
}
