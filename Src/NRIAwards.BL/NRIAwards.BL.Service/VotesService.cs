namespace NRIAwards.BL.Service;

public class VotesService : BaseService<Vote, int, VotesSearchParams, VotesOrderParams, VotesIncludeParams>,
	ICrudVotesService, IExtendedVotesService
{
	private readonly IExtendedVotesRepository _extendedVotesRepository;

	public VotesService(ICrudVotesRepository repository,
		IExtendedVotesRepository extendedVotesRepository
		) : base(repository)
	{
		_extendedVotesRepository = extendedVotesRepository;
	}
}

