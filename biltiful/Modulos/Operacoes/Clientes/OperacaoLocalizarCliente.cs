using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes
{
    internal class OperacaoLocalizarCliente
    {
        Arquivo<Cliente> arquivo;

        public OperacaoLocalizarCliente()
        {
            arquivo = new Arquivo<Cliente>(Constantes.DIRETORIO, Constantes.CLIENTE_ARQUIVO);
        }

        string EntradaCpf()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string cpf) => Cliente.VerificarCPF(cpf),
                "CPF inválido"
            );

            /// Isso acaba por pesquisar o mesmo valor duas vezes no arquivo
            entrada.AdicionarRegra(
                (string cpf) => arquivo.Ler().Find(c => c.CPF == cpf && c.Situacao == 'A') != null,
                "CPF não encontrado"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            List<Cliente> clientes = arquivo.Ler().Where(cliente => cliente.Situacao == 'A').ToList();

            if (clientes.Count <= 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o CPF que deseja localizar: ");
            string cpf = EntradaCpf();

            Cliente? cliente = clientes.Find(c => c.CPF == cpf);

            Console.WriteLine(cliente);

            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
        }
    }
}
