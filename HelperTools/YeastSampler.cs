namespace HelperTools;

public class YeastSampler
{
	public static void ResampleYeast(string path)
	{
		const string inFilename1 = @"FSY1742";
		const string inFilename2 = @"S288C";
		const string outFilename = @"combined-reads.fasta";
		
		const int totalReads1 = 249476;
		const int totalReads2 = 396326;
		const int chooseReads = 30000;

		HashSet<int> indexSet1 = ChooseIndexes(totalReads1, chooseReads);
		HashSet<int> indexSet2 = ChooseIndexes(totalReads2, chooseReads);
		HashSet<int> positionSet = ChooseIndexes(chooseReads * 2, chooseReads);

		int inIndex1 = 0;
		int inIndex2 = 0;
		int outIndex = 0;
		
		Console.WriteLine($"Start; Time: {DateTime.Now:HH:mm:ss}");

		using StreamReader inFile1 = new StreamReader(Path.Combine(path, inFilename1 + "-reads.fasta"));
		using StreamReader inFile2 = new StreamReader(Path.Combine(path, inFilename2 + "-reads.fasta"));
		using StreamWriter outFile = new StreamWriter(Path.Combine(path, outFilename));
		while (outIndex < chooseReads * 2)
		{
			if (positionSet.Contains(outIndex))
				inIndex1 = CopyNextRead(inFile1, outFile, inIndex1, indexSet1, inFilename1);
			else
				inIndex2 = CopyNextRead(inFile2, outFile, inIndex2, indexSet2, inFilename2);
			
			outIndex++;
			if (outIndex % 1000 == 0)
				Console.WriteLine($"Written strand: {outIndex}; Time: {DateTime.Now:HH:mm:ss}");
		}
		
		Console.WriteLine($"Done; Time: {DateTime.Now:HH:mm:ss}");
	}

	private static int CopyNextRead(StreamReader inFile, StreamWriter outFile, int strandIndex, HashSet<int> indexSet, string name)
	{
		bool copyStarted = false;

		while (true)
		{
			int firstCharacter = inFile.Peek();
			if (firstCharacter == -1)
				return strandIndex;

			if ((char)firstCharacter == '>' && copyStarted)
			{
				return strandIndex;
			}

			string? line = inFile.ReadLine();

			if ((char)firstCharacter == '>')
			{
				if (indexSet.Contains(strandIndex))
				{
					copyStarted = true;
					line = line + "--" + name;
				}
				strandIndex++;
			}

			if (copyStarted)
				outFile.WriteLine(line);
		}
	}

	private static HashSet<int> ChooseIndexes(int maxElement, int numberOfElements)
	{
		HashSet<int> indexes = new HashSet<int>();
		Random rnd = new Random(DateTime.Now.Millisecond);
		int currentTotal = maxElement;
		for (int i = 0; i < numberOfElements; i++)
		{
			int index = rnd.Next(currentTotal);
			currentTotal--;
			while (indexes.Contains(index))
			{
				index++;
				if (index >= maxElement) index = 0;
			}
			
			indexes.Add(index);
		}

		return indexes;
	}
}