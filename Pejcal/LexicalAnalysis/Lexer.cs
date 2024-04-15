namespace Pejcal.LexicalAnalysis;

public class Lexer
{
    private int _token;
    private int _indexOfToken;
    private readonly StreamReader _inputStream;
    private char _symbol;
    
    public Lexer(StreamReader file)
    {
        _inputStream = file;
    }
    private bool ReadNewToken()
    {
        if ((_token = _inputStream.Read()) == -1)
        {
            return false;
        }
        else
        {
            _indexOfToken++;
            return true;
        }
    }
    public void Analyze()
    {
        while ((ReadNewToken()))
        {
            _symbol = (char) _token;
            Console.WriteLine(_symbol);
        }
    }
    
    

}