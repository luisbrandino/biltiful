using biltiful.Classes;

string line = "01234567890123";

line += "EMPRESA".PadRight(50);

line += "010120000101200001012000A";

Fornecedor fornecedor = new(line);

Console.WriteLine(fornecedor.RazaoSocial + "B");