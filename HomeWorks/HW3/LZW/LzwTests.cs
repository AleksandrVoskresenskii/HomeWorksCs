using NUnit.Framework;
using System.IO;
using System.Text;

namespace LZW.Tests;

[TestFixture]
public class LzwTests
{
    private const string TestString = "TOBEORNOTTOBEORTOBEORNOT";

    [Test]
    public void CompressAndDecompress_ShouldReturnOriginalData()
    {
        byte[] original = Encoding.UTF8.GetBytes(TestString);

        using var compressedStream = new MemoryStream();
        LzwCompressor.Compress(original, compressedStream);

        byte[] compressed = compressedStream.ToArray();

        using var decompressedStream = new MemoryStream();
        LzwDecompressor.Decompress(compressed, decompressedStream);

        byte[] decompressed = decompressedStream.ToArray();

        Assert.That(decompressed, Is.EqualTo(original));
    }

    [Test]
    public void Compress_ShouldProduceSmallerOutputForRepetitiveData()
    {
        string input = new string('A', 1000);
        byte[] original = Encoding.UTF8.GetBytes(input);

        using var compressedStream = new MemoryStream();
        LzwCompressor.Compress(original, compressedStream);
        byte[] compressed = compressedStream.ToArray();

        Assert.That(compressed.Length, Is.LessThan(original.Length));
    }

    [Test]
    public void Decompress_InvalidData_ShouldThrow()
    {
        byte[] invalidData = { 0xFF, 0xFF, 0xFF };

        using var output = new MemoryStream();
        Assert.Throws<InvalidDataException>(() =>
        {
            LzwDecompressor.Decompress(invalidData, output);
        });
    }
}
