using Pejcal.LexicalAnalysis.interfaces;


namespace Pejcal.LexicalAnalysis;

public class LexerContext
{
    private ILexerState _state;
    public List<Event> Events = new List<Event>();
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
    private string _eventName = "";
    private void CheckNextSpace(LexerContext context)
    {
        GetNextToken(context);
        if (context.Token != ' ')
        {
            throw new LexicalException("Lexical error: Expected space, got " + context.Token);
        }
    }

    private void CheckName()
    {
        if(_eventName.Length == 0)
        {
            throw new LexicalException("Lexical error: Expected name, got " + _eventName);
        }
    }

    private void AddEventToList(LexerContext context)
    {
        context.Events.Add(new Event(_eventName));
    }
    private void GetName(LexerContext context)
    {
        try
        {
            while (GetNextToken(context))
            {
                if (PeekForLeftParenthesis(context))
                {
                    if (IsLetterOrDigit(context.Token))
                    {
                        _eventName += context.Token;
                    }
                    break;
                }
                if (IsLetterOrDigit(context.Token))
                {
                    _eventName += context.Token;
                }
            }
            CheckName();
            AddEventToList(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public override void Handle(LexerContext context, StreamReader inputStream)
    {
        CheckNextSpace(context);
        GetName(context);
    }
}

public class LetterState: BaseLexerState
{
    private string _createEvent = "";

    private bool CheckCreateEvent()
    {
        if(_createEvent == "createevent")
        {
            return true;
        }
        throw new LexicalException("Lexical error: Expected 'createevent', got " + _createEvent);
    }
    private void LoadCreateEvent(LexerContext context)
    {
        _createEvent += context.Token;
        for (int i = 0; i < 10; i++)
        {
            GetNextToken(context);
            _createEvent += context.Token;
        }
        _createEvent = _createEvent.ToLower();
        CheckCreateEvent();
    }
    public override void Handle(LexerContext context, StreamReader inputStream)
    {
        LoadCreateEvent(context);
        context.ChangeState(new CreateEventState());
        context.Handle(inputStream);
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