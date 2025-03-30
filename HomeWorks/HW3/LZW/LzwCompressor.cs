namespace LZW;

public static class LzwCompressor
{
        /// <summary>
    /// Compresses the specified input file using the Lempel–Ziv–Welch (LZW) algorithm and writes the result to the output file.
    /// Prints the compression ratio to the console.
    /// </summary>
    /// <param name="inputPath">The path to the input file to compress.</param>
    /// <param name="outputPath">The path where the compressed output should be written.</param>
    public static void Compress(string inputPath, string outputPath)
    {
        byte[] inputBytes = File.ReadAllBytes(inputPath);
        int originalSize = inputBytes.Length;

        var dict = new Dictionary<string, int>();
        for (int i = 0; i < 256; i++)
            dict[((char)i).ToString()] = i;

        List<ushort> outputCodes = new();
        string current = string.Empty;
        int code = 256;

        foreach (byte b in inputBytes)
        {
            char c = (char)b;
            string combined = current + c;
            if (dict.ContainsKey(combined))
            {
                current = combined;
            }
            else
            {
                outputCodes.Add((ushort)dict[current]);
                dict[combined] = code++;
                current = c.ToString();
            }
        }

        if (!string.IsNullOrEmpty(current))
            outputCodes.Add((ushort)dict[current]);

        using var bw = new BinaryWriter(File.Create(outputPath));
        foreach (ushort outputCode in outputCodes)
            bw.Write(outputCode);

        long compressedSize = new FileInfo(outputPath).Length;
        double ratio = (double)originalSize / compressedSize;
        Console.WriteLine($"Коэффициент сжатия: {ratio:F2}");
    }
    
}
