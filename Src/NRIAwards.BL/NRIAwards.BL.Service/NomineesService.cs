namespace NRIAwards.BL.Service;

public class NomineesService : BaseService<Nominee, int, NomineesSearchParams, NomineesOrderParams, NomineesIncludeParams>,
	ICrudNomineesService, IExtendedNomineesService
{
	private readonly IExtendedNomineesRepository _extendedNomineesRepository;

	public NomineesService(ICrudNomineesRepository repository,
		IExtendedNomineesRepository extendedNomineesRepository
		) : base(repository)
	{
		_extendedNomineesRepository = extendedNomineesRepository;
	}
}

