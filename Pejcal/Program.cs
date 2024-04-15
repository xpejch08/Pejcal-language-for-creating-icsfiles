using Pejcal.LexicalAnalysis;

class MainProgram
{
    static void Main(string [] args)
    {
        string path = args[0];
        FileHandler fileHandler = new FileHandler(path);
        Lexer lexer = new Lexer(fileHandler.InputFileStream);
        
        lexer.Analyze();
    }
}