namespace CesilEmulator
{
    public class CesilStorage
    {
        private readonly IReadOnlyList<int> _data;

        private readonly IDictionary<string, int> _variables = new Dictionary<string, int>();

        private int _dataIndex;

        public int Accumulator { get; set; }

        public CesilStorage(IReadOnlyList<int> data)
        {
            _data = data;
            _dataIndex = 0;
        }

        public bool IsVariable(string variable) => 
            _variables.ContainsKey(variable);

        public void Store(string variable)
        {
            if (IsVariable(variable))
            {
                _variables[variable] = Accumulator;
            }
            else
            {
                _variables.Add(variable, Accumulator);
            }
        }

        public int Get(string variable)
        {
            return IsVariable(variable)
                ? _variables[variable]
                : 0;
        }

        public int? GetNextDataItem()
        {
            if (_dataIndex >= _data.Count)
            {
                return null;
            }

            return _data[_dataIndex++];
        }

        public void Load(string value)
        {
            Accumulator = int.TryParse(value, out var result) 
                ? result 
                : Get(value);
        }

        public void Add(string value)
        {
            Accumulator += int.TryParse(value, out var result) 
                ? result 
                : Get(value);
        }

        public void Subtract(string value)
        {
            Accumulator -= int.TryParse(value, out var result) 
                ? result 
                : Get(value);
        }

        public void Multiply(string value)
        {
            Accumulator *= int.TryParse(value, out var result) 
                ? result 
                : Get(value);
        }

        public void Divide(string value)
        {
            Accumulator /= int.TryParse(value, out var result) 
                ? result 
                : Get(value);
        }
    }
}
