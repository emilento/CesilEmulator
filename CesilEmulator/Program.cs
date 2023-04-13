using CesilEmulator;

var code = new CesilCode();
var content =  await File.ReadAllLinesAsync("code.txt");
Array.ForEach(
    content,
    line =>
    {
        code.ParseLine(line);
    });

var interpreter = new CesilInterpreter(code);
CesilParserResult parserResult;

do
{
    parserResult = interpreter.Run();
    if (!string.IsNullOrWhiteSpace(parserResult.Message))
    {
        if (parserResult.Result == CesilParserResult.ParseResult.OK)
        {
            Console.ResetColor();
            Console.Write(parserResult.Message);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(parserResult.Message);
        }
    }
}
while (parserResult.Result == CesilParserResult.ParseResult.OK);
