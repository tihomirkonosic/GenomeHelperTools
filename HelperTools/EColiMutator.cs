using System.Text;

namespace HelperTools;

public class EColiMutator
{
    private class Sequence
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public string Data { get; set; }
        public Dictionary<char, int> Pile { get; set; }
    }

    public class Options
    {
        public int ReadLength { get; set; }
        public int ReadCount { get; set; }
        public int OverlapLength { get; set; }
    }

    public void Mutate(string inPath, string outPath, Options options)
    {
        Sequence mainData = ReadFile(inPath);
        List<Sequence> reads = CreateReads(mainData, options);
        WriteFile(reads, outPath);
    }

    private static void WriteFile(List<Sequence> reads, string outPath)
    {
        Console.WriteLine($"Writing start; Time: {DateTime.Now:HH:mm:ss}");

        using StreamWriter outFile = new StreamWriter(outPath);
        foreach (Sequence seq in reads)
        {
            outFile.WriteLine(">" + seq.Name);
            outFile.WriteLine(seq.Data);
        }

        Console.WriteLine($"Writing done; Time: {DateTime.Now:HH:mm:ss}");
    }

    private static List<Sequence> CreateReads(Sequence mainData, Options options)
    {
        Console.WriteLine($"Splitting start; Time: {DateTime.Now:HH:mm:ss}");

        List<Sequence> reads = [];
        int baseLength = options.ReadLength - options.OverlapLength;
        for (int k = 0; k < options.ReadCount; k++)
        {
            Sequence seq = new Sequence
            {
                Name = $"Read-{k + 1}",
                Data = mainData.Data.Substring(k * baseLength, options.ReadLength),
                Length = options.ReadLength
            };

            reads.Add(seq);
        }

        Console.WriteLine($"Splitting done; Time: {DateTime.Now:HH:mm:ss}");

        return reads;
    }

    private Sequence ReadFile(string inPath)
    {
        Console.WriteLine($"Reading start; Time: {DateTime.Now:HH:mm:ss}");

        Sequence res = new Sequence();

        string line;
        bool titleFound = false;
        StringBuilder sb = new StringBuilder("");
        res.Pile = new Dictionary<char, int>();

        using StreamReader inFile = new StreamReader(inPath);
        while ((line = inFile.ReadLine()) != null)
        {
            if (line.Length > 0 && line[0] == '>')
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
                res.Pile.TryAdd(c, 0);

                res.Pile[c]++;
            }
        }

        res.Data = sb.ToString();
        res.Length = res.Data.Length;

        Console.WriteLine($"Reading done; Time: {DateTime.Now:HH:mm:ss}");

        return res;
    }
}