using System;
using System.Diagnostics;
using System.IO;

namespace CodeGeneration.ServerCodeGenerator.Service.MergeUtility;

internal class TortoiseMergeUtility : MergeUtility, IMergeUtility
{
	protected override string DefaultExecutablePath => Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Program Files",
		"TortoiseSVN", "bin", "TortoiseMerge.exe");

	public override void PerformMerge(string baseFilePath, string file1, string file2, string mergedFilePath)
	{
		var process = Process.Start(ExecutablePath,
			$"/base:\"{baseFilePath}\" /theirs:\"{file1}\" /mine:\"{file2}\" /merged:\"{mergedFilePath}\"");
		process.WaitForExit();
	}
}