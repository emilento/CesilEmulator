namespace CesilEmulator
{
    public class CesilParser
    {
        private static readonly HashSet<string> ValidOperators = new HashSet<string>
        {
            "HALT",
            "LOAD",
            "STORE",
            "IN",
            "OUT",
            "ADD",
            "SUBTRACT",
            "MULTIPLY",
            "DIVIDE",
            "JUMP",
            "JIZERO",
            "JINEG",
            "PRINT",
            "LINE"
        };

        public bool IsOperatorValid(string op) =>
            ValidOperators.Any(item => item.Equals(op, StringComparison.CurrentCultureIgnoreCase));

        public enum ParseResult
        {
            ERROR = -1,
            OK = 0,
            HALT = 1,
        }

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
    }
}
