namespace NRIAwards.BL.Service;

public class ReadersService : BaseService<Reader, int, ReadersSearchParams, ReadersOrderParams, ReadersIncludeParams>,
	ICrudReadersService, IExtendedReadersService
{
	private readonly IExtendedReadersRepository _extendedReadersRepository;

	public ReadersService(ICrudReadersRepository repository,
		IExtendedReadersRepository extendedReadersRepository
		) : base(repository)
	{
		_extendedReadersRepository = extendedReadersRepository;
	}
}

