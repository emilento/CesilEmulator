namespace CesilEmulator;

public class CesilInterpreter
{
    private readonly CesilCode _code;

    private readonly CesilParser _parser = new();

    private readonly CesilStorage _storage;

    private bool _isRunning;

    public CesilInterpreter(CesilCode code)
    {
        _code = code;
        _storage = new CesilStorage(code.Data);
    }

    public CesilParserResult Run()
    {
        if (!_isRunning)
        {
            _isRunning = true;
        }
        
        var parserResult = _parser.Execute(_code.GetNextInstruction(), _storage, _code);
        if (parserResult.Result == CesilParserResult.ParseResult.ERROR
            || parserResult.Result == CesilParserResult.ParseResult.HALT)
        {
            _isRunning = false;
        }

        return parserResult;
    }
}