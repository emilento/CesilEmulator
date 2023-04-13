using System.Globalization;

namespace CesilEmulator;

public class CesilParser
{
    private static readonly HashSet<string> ValidOperators = new()
    {
        "ADD",
        "DIVIDE",
        "HALT",
        "IN",
        "JINEG",
        "JIZERO",
        "JUMP",
        "LINE",
        "LOAD",
        "MULTIPLY",
        "OUT",
        "PRINT",
        "STORE",
        "SUBTRACT"
    };

    public bool IsOperatorValid(string @operator) =>
        ValidOperators.Any(item => item.Equals(@operator, StringComparison.CurrentCultureIgnoreCase));

    public CesilParserResult Execute(
        CesilCodeLine? instruction,
        CesilStorage storage,
        CesilCode code)
    {
        var parserResult = new CesilParserResult();

        if (instruction == null)
        {
            parserResult.Result = CesilParserResult.ParseResult.HALT;
            return parserResult;
        }

        if (!IsOperatorValid(instruction.Operator))
        {
            parserResult.Result = CesilParserResult.ParseResult.ERROR;
            parserResult.Message = $"Invalid operator: {instruction.Operator}";
            return parserResult;
        }
        
        switch (instruction.Operator)
        {
            case "ADD":
                storage.Add(instruction.Operand);
                break;

            case "DIVIDE":
                storage.Divide(instruction.Operand);
                break;

            case "HALT":
                parserResult.Result = CesilParserResult.ParseResult.HALT;
                break;

            case "IN":
                var nextDataItem = storage.GetNextDataItem();
                if (nextDataItem != null)
                {
                    storage.Accumulator = nextDataItem.Value;
                    break;
                }

                parserResult.Result = CesilParserResult.ParseResult.ERROR;
                break;

            case "JINEG":
                if (storage.Accumulator < 0 && !code.JumpToLabel(instruction.Operand))
                {
                    parserResult.Result = CesilParserResult.ParseResult.ERROR;
                    parserResult.Message = $"Missing label: {instruction.Operand}";
                }

                break;

            case "JIZERO":
                if (storage.Accumulator == 0 && !code.JumpToLabel(instruction.Operand))
                {
                    parserResult.Result = CesilParserResult.ParseResult.ERROR;
                    parserResult.Message = $"Missing label: {instruction.Operand}";
                }

                break;

            case "JUMP":
                if (!code.JumpToLabel(instruction.Operand))
                {
                    parserResult.Result = CesilParserResult.ParseResult.ERROR;
                    parserResult.Message = $"Missing label: {instruction.Operand}";
                }

                break;

            case "LINE":
                parserResult.Message = Environment.NewLine;
                break;

            case "LOAD":
                storage.Load(instruction.Operand);
                break;

            case "MULTIPLY":
                storage.Multiply(instruction.Operand);
                break;

            case "OUT":
                parserResult.Message = storage.Accumulator.ToString(CultureInfo.InvariantCulture);
                break;

            case "PRINT":
                parserResult.Message = instruction.Operand;
                break;

            case "STORE":
                storage.Store(instruction.Operand);
                break;

            case "SUBTRACT":
                storage.Subtract(instruction.Operand);
                break;

            default:
                parserResult.Message = $"Unknown command: {instruction.Operand}";
                break;
        }

        parserResult.Result = CesilParserResult.ParseResult.OK;
        return parserResult;
    }
}
