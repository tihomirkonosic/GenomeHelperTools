namespace HelperTools;

internal class Program
{
	public static void Main(string[] args)
	{
		try
		{
			//const string yeastPath = @"D:\Development\GIS\test-data\yeast";
			//YeastSampler.ResampleYeast(yeastPath);

			const string coliPath = @"C:\Users\Tiho\Development\test\ecoli-sequence.fasta";
			const string outPath = @"C:\Users\Tiho\Development\test\out-reads-5K.fasta";
			
			EColiMutator.Options options = new()
			{
				ReadLength = 5000,
				OverlapLength = 1000,
				ReadCount = 10
			};
			
			new EColiMutator().Mutate(coliPath, outPath, options);
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
		}
	}
}