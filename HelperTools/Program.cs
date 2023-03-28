namespace HelperTools;

internal class Program
{
	public static void Main(string[] args)
	{
		try
		{
			//const string yeastPath = @"D:\Development\GIS\test-data\yeast";
			//YeastSampler.ResampleYeast(yeastPath);

			const string coliPath = @"D:\Development\GIS\test-data\ecoli\ecoli-reference.fasta";
			const string outPath = @"D:\Development\GIS\test-data\ecoli\ecoli-diploid-reads.fasta";
			new EColiMutator().Mutate(coliPath, outPath);
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
		}
	}
}