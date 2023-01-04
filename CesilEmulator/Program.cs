using CesilEmulator;

var cesilCode = new CesilCode();
var content =  await File.ReadAllLinesAsync("code.txt");
Array.ForEach(
    content,
    l =>
    {
        cesilCode.ParseLine(l);
    });

Console.WriteLine(cesilCode.ToString());
