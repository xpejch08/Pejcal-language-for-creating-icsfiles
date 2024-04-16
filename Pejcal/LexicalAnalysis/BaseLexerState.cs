using Pejcal.LexicalAnalysis.interfaces;

namespace Pejcal.LexicalAnalysis
{
    public abstract class BaseLexerState : ILexerState
    {
        private int _token;
        public abstract void Handle(LexerContext context, StreamReader inputStream);
        
        protected bool IsLetter(char token)
        {
            return char.IsLetter(token);
        }
        protected bool IsDigit(char token)
        {
            return char.IsDigit(token);
        }
        protected bool IsLetterOrDigit(char token)
        {
            return char.IsLetterOrDigit(token);
        }
        protected bool PeekForLeftParenthesis(LexerContext context)
        {
            if (context.InputStream.Peek() != -1)
            {
                return (char)context.InputStream.Peek() == '(';
            }
            return false;
        }
        protected bool IsWhiteSpace(char token)
        {
            return char.IsWhiteSpace(token);
        }
        private bool IsNextCharWhiteSpace(LexerContext context)
        {
            if(context.InputStream.Peek() != -1)
            {
                return IsWhiteSpace((char)context.InputStream.Peek());
            }
            return false;
        }
        private bool ReadNextChar(LexerContext context)
        {
            if ((_token = context.InputStream.Read()) == -1)
            { 
                return false;
            }
            return true;
        }
        private void AssignContextToken(LexerContext context)
        {
            context.Token = (char)_token;
            context.Token = char.ToLower(context.Token);
        }
        protected bool GetNextToken(LexerContext context)
        {
            try
            {
                if (!ReadNextChar(context))
                { 
                    return false;
                }
                AssignContextToken(context);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new SystemException("System error: Can't read from file");
            }
        }
    }
}