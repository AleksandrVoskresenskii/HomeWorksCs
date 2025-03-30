using System;
using LZW;

namespace LZWApp;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: dotnet run <file path> <-c|-u>");
            return;
        }

        string filePath = args[0];
        string option = args[1];

        try
        {
            if (option == "-c")
            {
                string outputPath = filePath + ".zipped";
                LzwCompressor.Compress(filePath, outputPath);
                Console.WriteLine("File compressed to: " + outputPath);
            }
            else if (option == "-u")
            {
                if (!filePath.EndsWith(".zipped"))
                {
                    Console.Error.WriteLine("Compressed file must have .zipped extension.");
                    return;
                }

                string outputPath = filePath.Substring(0, filePath.Length - ".zipped".Length);
                LzwDecompressor.Decompress(filePath, outputPath);
                Console.WriteLine("File decompressed to: " + outputPath);
            }
            else
            {
                Console.Error.WriteLine("Invalid option. Use -c to compress or -u to decompress.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}
