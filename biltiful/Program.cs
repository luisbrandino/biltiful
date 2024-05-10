using biltiful;
using biltiful.Classes;

Arquivo<Cliente> arquivo = new(Constantes.DIRETORIO + Constantes.CLIENTE_ARQUIVO);

List<Cliente> clientes = arquivo.Ler();

if (clientes.Count == 0)
{
    Console.WriteLine("Sem entradas no arquivo");
    Environment.Exit(0);
}

foreach (Cliente cliente in clientes)
    Console.WriteLine(cliente.CPF);

arquivo.Inserir(new Cliente("52202188835", "Luis Brandino", new DateOnly(2005, 5, 19), 'M'));