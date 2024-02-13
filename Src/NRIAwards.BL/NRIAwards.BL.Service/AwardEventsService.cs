namespace NRIAwards.BL.Service;

public class AwardEventsService : BaseService<AwardEvent, int, AwardEventsSearchParams, AwardEventsOrderParams, AwardEventsIncludeParams>,
	ICrudAwardEventsService, IExtendedAwardEventsService
{
	private readonly IExtendedAwardEventsRepository _extendedAwardEventsRepository;

	public AwardEventsService(ICrudAwardEventsRepository repository,
		IExtendedAwardEventsRepository extendedAwardEventsRepository
		) : base(repository)
	{
		_extendedAwardEventsRepository = extendedAwardEventsRepository;
	}
}

