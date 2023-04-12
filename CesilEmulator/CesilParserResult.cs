namespace CesilEmulator;

public class CesilParserResult
{
    public string Message { get; set; } = string.Empty;

    public ParseResult Result { get; set; }

    public enum ParseResult
    {
        ERROR = -1,
        OK = 0,
        HALT = 1,
    }
}
