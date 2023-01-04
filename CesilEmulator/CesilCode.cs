using System.Globalization;
using System.Text;

namespace CesilEmulator
{
    public class CesilCode
    {
        private readonly List<string> _raw;

        private readonly List<CesilCodeLine> _codeLines;

        private readonly List<int> _data;

        private bool _isData;

        public CesilCode()
        {
            _raw = new List<string>();
            _codeLines = new List<CesilCodeLine>();
            _data = new List<int>();
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
                    _data.Add(number);
                }
                else
                {
                    _codeLines.Add(new CesilCodeLine(string.Empty, segments[0], string.Empty, _raw.Count));
                }
            }

            if (segments.Length == 2 && !_isData)
            {
                _codeLines.Add(new CesilCodeLine(string.Empty, segments[0], segments[1], _raw.Count));
            }

            if (segments.Length == 3 && !_isData)
            {
                _codeLines.Add(new CesilCodeLine(segments[0], segments[1], segments[2], _raw.Count));
            }
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            foreach (var codeLine in _codeLines)
            {
                output.AppendLine($"{codeLine.Label}\t{codeLine.Instruction}\t{codeLine.Operand}");
            }

            output.AppendLine();
            output.AppendLine("%DATA*");
            foreach (var data in _data)
            {
                output.AppendLine(data.ToString(CultureInfo.InvariantCulture));
            }

            return output.ToString();
        }
    }
}
