namespace NRIAwards.BL.Service;

public class AwardsService : BaseService<Award, int, AwardsSearchParams, AwardsOrderParams, AwardsIncludeParams>,
	ICrudAwardsService, IExtendedAwardsService
{
	private readonly IExtendedAwardsRepository _extendedAwardsRepository;

	public AwardsService(ICrudAwardsRepository repository,
		IExtendedAwardsRepository extendedAwardsRepository
		) : base(repository)
	{
		_extendedAwardsRepository = extendedAwardsRepository;
	}
}

