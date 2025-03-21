using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LZWCompression;

public class LZW
{
    private const int InitialDictionarySize = 256;

    /// <summary>
    /// Compresses a file using the LZW algorithm.
    /// </summary>
    /// <param name="inputFile">Path to the input file.</param>
    /// <param name="outputFile">Path to the output compressed file.</param>
    public static void Compress(string inputFile, string outputFile)
    {
        var data = File.ReadAllBytes(inputFile);
        var dict = new Dictionary<string, int>();

        for (int i = 0; i < InitialDictionarySize; i++)
            dict.Add(((char)i).ToString(), i);

        string current = string.Empty;
        var result = new List<int>();

        foreach (var b in data)
        {
            string combined = current + (char)b;
            if (dict.ContainsKey(combined))
                current = combined;
            else
            {
                result.Add(dict[current]);
                dict[combined] = dict.Count;
                current = ((char)b).ToString();
            }
        }

        if (!string.IsNullOrEmpty(current))
            result.Add(dict[current]);

        using (var writer = new BinaryWriter(File.OpenWrite(outputFile)))
        {
            foreach (var index in result)
                writer.Write(index);
        }

        var compressionRatio = (double)new FileInfo(inputFile).Length / new FileInfo(outputFile).Length;
        Console.WriteLine($"Compression ratio: {compressionRatio:F2}");
    }

    /// <summary>
    /// Decompresses a file using the LZW algorithm.
    /// </summary>
    /// <param name="inputFile">Path to the compressed input file.</param>
    /// <param name="outputFile">Path to the output decompressed file.</param>
    public static void Decompress(string inputFile, string outputFile)
    {
        var compressedData = new List<int>();

        using (var reader = new BinaryReader(File.OpenRead(inputFile)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
                compressedData.Add(reader.ReadInt32());
        }

        var dict = new Dictionary<int, string>();
        for (int i = 0; i < InitialDictionarySize; i++)
            dict.Add(i, ((char)i).ToString());

        string current = dict[compressedData[0]];
        compressedData.RemoveAt(0);
        var result = new List<byte>(System.Text.Encoding.UTF8.GetBytes(current));

        foreach (var code in compressedData)
        {
            string entry;
            if (dict.ContainsKey(code))
                entry = dict[code];
            else if (code == dict.Count)
                entry = current + current[0];
            else
                throw new Exception("Invalid compressed code.");

            result.AddRange(System.Text.Encoding.UTF8.GetBytes(entry));
            dict[dict.Count] = current + entry[0];
            current = entry;
        }

        File.WriteAllBytes(outputFile, result.ToArray());
    }

    /// <summary>
    /// Performs the Burrows-Wheeler Transform on the input string.
    /// </summary>
    /// <param name="input">The string to transform.</param>
    /// <returns>A tuple containing the transformed string and index of original string.</returns>
    public static (string transformed, int index) BWTTransform(string input)
    {
        if (!input.EndsWith("$"))
        {
            input += '$';
        }

        int n = input.Length;
        int[] indices = Enumerable.Range(0, n).ToArray();

        Array.Sort(indices, (i, j) =>
        {
            for (int k = 0; k < n; k++)
            {
                char ci = input[(i + k) % n];
                char cj = input[(j + k) % n];
                if (ci != cj)
                {
                    return ci - cj;
                }
            }
            return 0;
        });

        char[] lastColumn = new char[n];
        int originalIndex = -1;

        for (int i = 0; i < n; i++)
        {
            lastColumn[i] = input[(indices[i] + n - 1) % n];
            if (indices[i] == 0)
            {
                originalIndex = i;
            }
        }

        return (new string(lastColumn), originalIndex);
    }

    /// <summary>
    /// Performs the inverse Burrows-Wheeler Transform to recover the original string.
    /// </summary>
    /// <param name="bwt">The transformed string.</param>
    /// <param name="index">The index of original string.</param>
    /// <returns>The original string before transformation.</returns>
    public static string BWTInverseTransform(string bwt, int index)
    {
        int n = bwt.Length;
        int[] count = new int[256];
        int[] firstOccurrence = new int[256];

        foreach (char c in bwt)
        {
            count[c]++;
        }

        int sum = 0;
        for (int i = 0; i < 256; i++)
        {
            firstOccurrence[i] = sum;
            sum += count[i];
        }

        int[] next = new int[n];
        int[] tempCount = new int[256];

        for (int i = 0; i < n; i++)
        {
            char c = bwt[i];
            next[i] = firstOccurrence[c] + tempCount[c];
            tempCount[c]++;
        }

        char[] original = new char[n];
        int pos = index;

        for (int i = n - 1; i >= 0; i--)
        {
            original[i] = bwt[pos];
            pos = next[pos];
        }

        return new string(original).TrimEnd('$');
    }
}
