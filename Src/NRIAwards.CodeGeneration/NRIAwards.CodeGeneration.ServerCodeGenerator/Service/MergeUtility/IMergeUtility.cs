namespace CodeGeneration.ServerCodeGenerator.Service.MergeUtility;

internal interface IMergeUtility
{
	void PerformMerge(string baseFilePath, string file1, string file2, string mergedFilePath);
}