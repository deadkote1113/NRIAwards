using System.IO;

namespace CodeGeneration.ServerCodeGenerator.Service.MergeUtility;

internal abstract class MergeUtility
{
	private string path;

	protected virtual string DefaultExecutablePath { get; }

	internal virtual string ExecutablePath
	{
		get => path ?? DefaultExecutablePath;
		set
		{
			if (value != null && !File.Exists(value))
				throw new FileNotFoundException($"Файл {value} не существует.");
			path = value;
		}
	}

	public abstract void PerformMerge(string baseFilePath, string file1, string file2, string mergedFilePath);
}