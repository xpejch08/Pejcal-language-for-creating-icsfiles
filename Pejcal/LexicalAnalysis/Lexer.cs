namespace Pejcal.LexicalAnalysis;

public class Lexer
{
    private int _token;
    private int _indexOfToken;
    private readonly StreamReader _inputStream;
    private char _symbol;
    private string createEvent = "createevent";
    
    public Lexer(StreamReader file)
    {
        _inputStream = file;
    }
       public void Analyze()
    {
        try
        {
            LexerContext context = new LexerContext(new InitialState(), _inputStream);
            context.Handle(_inputStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}