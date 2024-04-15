namespace Pejcal.LexicalAnalysis;
using System.IO;

public class FileHandler
{
    private readonly string _pathToFile;
    public StreamReader InputFileStream;
    
    public  FileHandler(string path)
    {
        _pathToFile = path;
        InputFileStream = OpenFileStream();
    }
    private StreamReader OpenFileStream()
    {
        try
        {
            InputFileStream = new StreamReader(_pathToFile);
            return InputFileStream;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}