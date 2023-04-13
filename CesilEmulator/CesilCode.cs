using System.Globalization;
using System.Text;

namespace CesilEmulator;

public class CesilCode
{
    private readonly List<string> _raw;

    public List<CesilCodeLine> CodeLines { get; }

    public List<int> Data { get; }

    private bool _isData;

    private int _currentLineIndex;

    public CesilCode()
    {
        _currentLineIndex = 0;
        _raw = new List<string>();
        CodeLines = new List<CesilCodeLine>();
        Data = new List<int>();
    }

    public CesilCodeLine? GetNextInstruction()
    {
        if (_currentLineIndex >= CodeLines.Count)
        {
            _currentLineIndex = 0;
            return null;
        }

        return CodeLines[_currentLineIndex++];
    }

    public bool JumpToLabel(string label)
    {
        int index = CodeLines.FindIndex((Predicate<CesilCodeLine>)(element => element.Label.Equals(label, StringComparison.CurrentCultureIgnoreCase)));
        if (index < 0)
        {
            return false;
        }

        _currentLineIndex = index;
        return true;
    }

    public void ParseLine(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return;
        }

        _raw.Add(line);

        var segments = line.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0)
        {
            return;
        }

        if (segments.Length == 1)
        {
            if (!_isData && segments[0] == "%")
            {
                _isData = true;
            }
            else if (_isData && segments[0] == "*")
            {
                _isData = false;
            }
            else if (_isData && int.TryParse(segments[0], out var number))
            {
                Data.Add(number);
            }
            else
            {
                CodeLines.Add(new CesilCodeLine(string.Empty, segments[0], string.Empty, _raw.Count));
            }
        }

        if (segments.Length == 2 && !_isData)
        {
            CodeLines.Add(new CesilCodeLine(string.Empty, segments[0], segments[1], _raw.Count));
        }

        if (segments.Length == 3 && !_isData)
        {
            CodeLines.Add(new CesilCodeLine(segments[0], segments[1], segments[2], _raw.Count));
        }
    }

    public override string ToString()
    {
        var output = new StringBuilder();

        foreach (var codeLine in CodeLines)
        {
            output.AppendLine($"{codeLine.Label}\t{codeLine.Operator}\t{codeLine.Operand}");
        }

        output.AppendLine();
        output.AppendLine("%DATA*");
        foreach (var data in Data)
        {
            output.AppendLine(data.ToString(CultureInfo.InvariantCulture));
        }

        return output.ToString();
    }
}