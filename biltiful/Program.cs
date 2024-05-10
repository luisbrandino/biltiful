using biltiful.Classes;

string line = "MP0001";

line += "Vaselina".PadRight(20);
line += "0101100001011000A";

MPrima m = new(line);

Console.WriteLine(m.Situacao);