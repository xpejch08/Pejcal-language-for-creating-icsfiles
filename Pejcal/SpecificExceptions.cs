namespace Pejcal;

public class SystemException: Exception
{
    public SystemException()
    {
        
    }
    public SystemException(string message): base(message)
    {
        
    }
    public SystemException(string message, Exception innerException): base(message, innerException)
    {
        
    }
    
}
public class LexicalException: Exception
{
    public LexicalException()
    {
        
    }
    public LexicalException(string message): base(message)
    {
        
    }
    public LexicalException(string message, Exception innerException): base(message, innerException)
    {
        
    }
    
}