using System.Globalization;
using System.Text;

namespace CesilEmulator
{
    public class CesilCode
    {
        private readonly List<string> _raw;

        public List<CesilCodeLine> CodeLines { get; }

        public List<int> Data { get; }

        private bool _isData;

        public CesilCode()
        {
            _raw = new List<string>();
            CodeLines = new List<CesilCodeLine>();
            Data = new List<int>();
        }

        public void ParseLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            _raw.Add(line);

            var segments = line.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
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
}
