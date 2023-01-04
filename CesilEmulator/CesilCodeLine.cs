namespace CesilEmulator
{
    public class CesilCodeLine
    {
        public string Label { get; }

        public string Instruction { get; }

        public string Operand { get; }

        public int LineNumber { get; }

        public CesilCodeLine(string label, string instruction, string operand, int lineNumber)
        {
            Label = label;
            Instruction = instruction;
            Operand = operand;
            LineNumber = lineNumber;
        }
    }
}
