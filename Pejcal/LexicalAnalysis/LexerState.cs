using Pejcal.LexicalAnalysis.interfaces;


namespace Pejcal.LexicalAnalysis;

public class LexerContext
{
    private ILexerState _state;
    public char Token = ' ';
    public StreamReader InputStream;
    
    public LexerContext(ILexerState initialState, StreamReader inputStream)
    {
        _state = initialState;
        InputStream = inputStream;
    }
    
    public void ChangeState(ILexerState newState)
    {
        _state = newState;
    }
    
    public void Handle(StreamReader inputStream)
    {
        _state.Handle(this, inputStream);
    }
}

public class CreateEventState : BaseLexerState
{
    public override void Handle(LexerContext context, StreamReader inputStream)
    {
        throw new NotImplementedException();
    }
}

public class LetterState: BaseLexerState
{
    private string _createEvent = "";
    
    private bool LoadCreateEvent(LexerContext context)
    {
        _createEvent += context.Token;
        for (int i = 0; i < 10; i++)
        {
            GetNextToken(context);
            _createEvent += context.Token;
        }
        _createEvent = _createEvent.ToLower();
        if (_createEvent == "createevent")
        {
            return true;
        }

        return false;
    }
    public override void Handle(LexerContext context, StreamReader inputStream)
    {
        if (!LoadCreateEvent(context))
        {
            throw new LexicalException("Lexical error: Expected 'createevent', got " + _createEvent);
        }
        context.ChangeState(new CreateEventState());
    }
}

public class InitialState: BaseLexerState
{
    public override void Handle(LexerContext context, StreamReader inputStream)
    {
        while (IsWhiteSpace(context.Token))
        {
            GetNextToken(context);
        }
        {
            
        }
        if (!IsLetter(context.Token))
        {
            throw new Pejcal.LexicalException("Lexical error: Expected letter, got " + context.Token);
        }
        else
        {
            context.ChangeState(new LetterState());
            context.Handle(inputStream);
        }
    }
}