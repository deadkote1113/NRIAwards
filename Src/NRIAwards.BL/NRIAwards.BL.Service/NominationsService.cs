namespace NRIAwards.BL.Service;

public class NominationsService : BaseService<Nomination, int, NominationsSearchParams, NominationsOrderParams, NominationsIncludeParams>,
	ICrudNominationsService, IExtendedNominationsService
{
	private readonly IExtendedNominationsRepository _extendedNominationsRepository;

	public NominationsService(ICrudNominationsRepository repository,
		IExtendedNominationsRepository extendedNominationsRepository
		) : base(repository)
	{
		_extendedNominationsRepository = extendedNominationsRepository;
	}
}

