namespace CesilEmulator;

public class CesilCodeLine
{
    public string Label { get; }

    public string Operator { get; }

    public string Operand { get; }

    public int LineNumber { get; }

    public CesilCodeLine(string label, string @operator, string operand, int lineNumber)
    {
        Label = label;
        Operator = @operator;
        Operand = operand;
        LineNumber = lineNumber;
    }
}