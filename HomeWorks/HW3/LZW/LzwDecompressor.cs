namespace LZW;

public static class LzwDecompressor
{
    /// <summary>
    /// Decompresses a file previously compressed with the LZW algorithm.
    /// </summary>
    /// <param name="inputPath">Path to the input compressed file.</param>
    /// <param name="outputPath">Path where the decompressed output should be written.</param>
    public static void Decompress(string inputPath, string outputPath)
    {
        byte[] compressedData = File.ReadAllBytes(inputPath);
        List<int> compressedCodes = new();

        for (int i = 0; i < compressedData.Length; i += 2)
        {
            int code = compressedData[i] | (compressedData[i + 1] << 8);
            compressedCodes.Add(code);
        }

        Dictionary<int, string> dictionary = new();
        for (int i = 0; i < 256; i++)
        {
            dictionary[i] = ((char)i).ToString();
        }

        int dictSize = 256;
        string previous = dictionary[compressedCodes[0]];
        List<byte> output = new();
        output.AddRange(System.Text.Encoding.UTF8.GetBytes(previous));

        for (int i = 1; i < compressedCodes.Count; i++)
        {
            int currentCode = compressedCodes[i];
            string entry;

            if (dictionary.ContainsKey(currentCode))
            {
                entry = dictionary[currentCode];
            }
            else if (currentCode == dictSize)
            {
                entry = previous + previous[0];
            }
            else
            {
                throw new InvalidDataException("Invalid LZW code encountered during decompression.");
            }

            output.AddRange(System.Text.Encoding.UTF8.GetBytes(entry));

            dictionary[dictSize++] = previous + entry[0];
            previous = entry;
        }

        File.WriteAllBytes(outputPath, output.ToArray());
    }
}
