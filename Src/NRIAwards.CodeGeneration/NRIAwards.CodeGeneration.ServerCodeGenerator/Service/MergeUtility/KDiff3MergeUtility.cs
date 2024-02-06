using System;
using System.Diagnostics;
using System.IO;

namespace CodeGeneration.ServerCodeGenerator.Service.MergeUtility;

internal class KDiff3MergeUtility : MergeUtility, IMergeUtility
{
	protected override string DefaultExecutablePath => Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Program Files",
		"KDiff3", "kdiff3.exe");

	public override void PerformMerge(string baseFilePath, string file1, string file2, string mergedFilePath)
	{
		var process = Process.Start(ExecutablePath,
			$"\"{baseFilePath}\" \"{file1}\" \"{file2}\" -o \"{mergedFilePath}\" --auto --cs \"AutoAdvance=1\"");
		process.WaitForExit();
	}
}