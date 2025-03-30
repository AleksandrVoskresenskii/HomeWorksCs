namespace LZW;

/// <summary>
/// Provides helper methods for file input/output operations and optional BWT transformations.
/// </summary>
public static class Utils
{
    /// <summary>
    /// Reads all bytes from a file.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    /// <returns>The contents of the file as a byte array.</returns>
    public static byte[] ReadBytes(string path)
    {
        return File.ReadAllBytes(path);
    }

    /// <summary>
    /// Writes bytes to a file.
    /// </summary>
    /// <param name="path">The path to the file.</param>
    /// <param name="data">The byte array to write.</param>
    public static void WriteBytes(string path, byte[] data)
    {
        File.WriteAllBytes(path, data);
    }

    /// <summary>
    /// Applies BWT transform to the input and encodes index as prefix.
    /// </summary>
    /// <param name="input">The string to transform.</param>
    /// <returns>The transformed string with index prefix.</returns>
    public static string ApplyBwt(string input)
    {
        var (transformed, index) = BWT.BurrowsWheelerTransform.Transform(input);
        return index.ToString("D4") + transformed;
    }

    /// <summary>
    /// Reverses a BWT-transformed string assuming the index is encoded in the prefix.
    /// </summary>
    /// <param name="input">The BWT-transformed string with index prefix.</param>
    /// <returns>The original untransformed string.</returns>
    public static string ReverseBwt(string input)
    {
        int index = int.Parse(input.Substring(0, 4));
        string bwt = input.Substring(4);
        return BWT.BurrowsWheelerTransform.InverseTransform(bwt, index);
    }

    /// <summary>
    /// Calculates the compression ratio as original size divided by compressed size.
    /// </summary>
    /// <param name="originalSize">The size of the original data.</param>
    /// <param name="compressedSize">The size of the compressed data.</param>
    /// <returns>The compression ratio.</returns>
    public static double CalculateCompressionRatio(long originalSize, long compressedSize)
    {
        if (compressedSize == 0)
        {
            return 0.0;
        }

        return (double)originalSize / compressedSize;
    }
}
