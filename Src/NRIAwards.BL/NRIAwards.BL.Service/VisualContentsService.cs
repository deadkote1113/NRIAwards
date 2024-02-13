namespace NRIAwards.BL.Service;

public class VisualContentsService : BaseService<VisualContent, int, VisualContentsSearchParams, VisualContentsOrderParams, VisualContentsIncludeParams>,
	ICrudVisualContentsService, IExtendedVisualContentsService
{
	private readonly IExtendedVisualContentsRepository _extendedVisualContentsRepository;

	public VisualContentsService(ICrudVisualContentsRepository repository,
		IExtendedVisualContentsRepository extendedVisualContentsRepository
		) : base(repository)
	{
		_extendedVisualContentsRepository = extendedVisualContentsRepository;
	}
}

