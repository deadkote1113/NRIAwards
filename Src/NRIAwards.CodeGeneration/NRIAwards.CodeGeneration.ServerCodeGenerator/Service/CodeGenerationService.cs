using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGeneration.ServerCodeGenerator.Service;

internal class CodeGenerationService : BackgroundService
{
	private readonly ILogger<CodeGenerationService> _logger;
	private readonly CodeGenerator _generator;
	private readonly IHostApplicationLifetime _hostApplicationLifetime;

	public CodeGenerationService(ILogger<CodeGenerationService> logger, CodeGenerator generator, IHostApplicationLifetime hostApplicationLifetime)
	{
		_logger = logger;
		_generator = generator;
		_hostApplicationLifetime = hostApplicationLifetime;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogWarning("Начинаю работать");
		_generator.Generate();
		_logger.LogWarning("Завершено");
		for (int i = 10; i > 0; i--)
		{
			_logger.LogWarning($"я Закроюсь через {i}");
			await Task.Delay(1000);
		}
		_hostApplicationLifetime.StopApplication();
	}
}
