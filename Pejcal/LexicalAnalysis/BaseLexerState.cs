using Pejcal.LexicalAnalysis.interfaces;

namespace Pejcal.LexicalAnalysis
{
    public abstract class BaseLexerState : ILexerState
    {
        public abstract void Handle(LexerContext context, StreamReader inputStream);
        
        protected bool IsLetter(char token)
        {
            return char.IsLetter(token);
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
        protected bool GetNextToken(LexerContext context)
        {
            try
            {
                int token;
                if ((token = context.InputStream.Read()) == -1)
                { 
                    return false;
                }
                context.Token = (char)token;
                context.Token = char.ToLower(context.Token);
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