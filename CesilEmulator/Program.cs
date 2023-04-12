using CesilEmulator;

var cesilCode = new CesilCode();
var content =  await File.ReadAllLinesAsync("code.txt");
Array.ForEach(
    content,
    line =>
    {
        cesilCode.ParseLine(line);
    });

Console.WriteLine(cesilCode.ToString());

var storage = new CesilStorage(cesilCode.Data);
var parser = new CesilParser();
parser.Execute(cesilCode, storage);

Console.WriteLine(storage.Accumulator);
