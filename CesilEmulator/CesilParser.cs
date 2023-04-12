namespace CesilEmulator;

public class CesilParser
{
    private const string ErrorMissingOperator = "Missing Operator";
    private const string ErrorUnknownOperator = "What is {0} ?";
    private const string ErrorMissingOperand = "Operator {0} requires an operand";
    private const string ErrorUnexpectedOperand = "Unexpected Operand: {0}";
    private const string ErrorBadOperand = "Invalid Operand: {0}";
    private const string ErrorBadLabel = "Bad Label: {0}";

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
    
    public enum OperandExpectedType
    {
        NONE,
        LABEL,
        IDENTIFIER,
        LITERAL,
        IDENTIFIER_OR_LITERAL,
        STRING,
        UNKNOWN,
    }

    public CesilParserResult Execute(CesilCode cesilCode, CesilStorage storage)
    {
        var result = new CesilParserResult();

        foreach (var line in cesilCode.CodeLines)
        {
            switch (line.Operator)
            {
                case "ADD":
                    storage.Add(line.Operand);
                    break;

                case "DIVIDE":
                    storage.Divide(line.Operand);
                    break;

                case "HALT":
                    result.Result = CesilParserResult.ParseResult.HALT;
                    return result;

                case "IN":
                    var nextDataItem = storage.GetNextDataItem();
                    if (nextDataItem != null)
                    {
                        storage.Accumulator = nextDataItem.Value;
                        break;
                    }

                    result.Result = CesilParserResult.ParseResult.ERROR;
                    return result;

                case "JINEG":
                    break;
                
                case "JIZERO":
                    break;

                case "JUMP":
                    break;

                case "LINE":
                    break;

                case "LOAD":
                    storage.Load(line.Operand);
                    break;

                case "MULTIPLY":
                    storage.Multiply(line.Operand);
                    break;

                case "OUT":
                    break;

                case "PRINT":
                    break;

                case "STORE":
                    storage.Store(line.Operand);
                    break;

                case "SUBTRACT":
                    storage.Subtract(line.Operand);
                    break;

                default:
                    break;
            }
        }

        result.Result = CesilParserResult.ParseResult.OK;
        return result;
    }
}
