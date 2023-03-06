namespace FastaSampler;

internal class Program
{
	public static void Main(string[] args)
	{
		try
		{
			const string path = @"D:\Development\GIS\test-data\yeast";
			
			YeastSampler.ResampleYeast(path);
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
		}
	}
}