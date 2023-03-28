using System.Text;

namespace HelperTools;

public class EColiMutator
{
	public class Sequence
	{
		public string Name { get; set; }
		public int Length { get; set; }
		public string Data { get; set; }
		public Dictionary<char, int> Pile { get; set; }
	}
	
	public void Mutate(string inPath, string outPath)
	{
		//using StreamWriter outFile = new StreamWriter(outPath);
		Sequence data = ReadFile(inPath);
	}

	private Sequence ReadFile(string inPath)
	{
		Console.WriteLine($"reading start; Time: {DateTime.Now:HH:mm:ss}");

		Sequence res = new Sequence();

		string line;
		bool titleFound = false;
		StringBuilder sb = new StringBuilder("");
		res.Pile = new Dictionary<char, int>();

		using StreamReader inFile = new StreamReader(inPath);
		while ((line = inFile.ReadLine()) != null)
		{
			if (line[0] == '>')
			{
				if (titleFound) 
					throw new Exception("Only one sequence is supported");
				
				titleFound = true;
				res.Name = line.Substring(1);
				continue;
			}
			
			if (!titleFound)
				continue;

			sb.Append(line);

			foreach (char c in line)
			{
				if (!res.Pile.ContainsKey(c))
					res.Pile.Add(c, 0);

				res.Pile[c]++;
			}
		}

		res.Data = sb.ToString();
		res.Length = res.Data.Length;
		
		Console.WriteLine($"Reading done; Time: {DateTime.Now:HH:mm:ss}");

		return res;
	}
}