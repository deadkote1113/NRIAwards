namespace NRIAwards.BL.Service;

public class AwardSessionsService : BaseService<AwardSession, int, AwardSessionsSearchParams, AwardSessionsOrderParams, AwardSessionsIncludeParams>,
	ICrudAwardSessionsService, IExtendedAwardSessionsService
{
	private readonly IExtendedAwardSessionsRepository _extendedAwardSessionsRepository;

	public AwardSessionsService(ICrudAwardSessionsRepository repository,
		IExtendedAwardSessionsRepository extendedAwardSessionsRepository
		) : base(repository)
	{
		_extendedAwardSessionsRepository = extendedAwardSessionsRepository;
	}
}

