namespace Pejcal.LexicalAnalysis.interfaces;

public interface ILexerState
{
    void Handle(LexerContext context, StreamReader inputStream);
    
}