using biltiful.Modulos.Operacoes.Entradas;
using biltiful.Classes;

namespace biltiful.Modulos.Operacoes
{
    internal class OperacaoCadastroCliente
    {
        public string EntradaCpf()
        {
            Entrada<string> entrada = new();

            entrada.DefinirMensagemDeErro("CPF inválido");

            entrada.AdicionarRegra((string cpf) => Cliente.VerificarCPF(cpf));

            return entrada.Pegar();
        }

        public string EntradaNome()
        {
            Entrada<string> entrada = new();

            entrada.DefinirMensagemDeErro("Nome precisa ter entre 1 e 50 caracteres");

            entrada.AdicionarRegra((string nome) => nome.Length >= 1 && nome.Length <= 50);

            return entrada.Pegar();
        }

        public DateOnly EntradaDataNascimento()
        {
            Entrada<DateOnly> entrada = new();

            entrada.DefinirMensagemDeErro("Data inválida.");
            entrada.DefinirMensagemDeUso("Uso: dd/mm/aaaa");

            entrada.AdicionarRegra((DateOnly data) => Cliente.VerificarDataDeNascimento(data));

            return entrada.Pegar();
        }

        public char EntradaSexo()
        {
            Entrada<char> entrada = new();

            entrada.DefinirMensagemDeErro("Sexo inválida.");
            entrada.DefinirMensagemDeUso("Uso: M/F");

            entrada.AdicionarRegra((char sexo) => Cliente.VerificarSexo(sexo));

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            DateOnly? date = (DateOnly?)Convert.ChangeType(Console.ReadLine(), typeof(DateOnly));

            Console.WriteLine(date);

            Console.Write("Informe o CPF: ");
            string cpf = EntradaCpf();

            Console.Write("Informe o nome: ");
            string nome = EntradaNome();

            Console.Write("Digite a data de nascimento (dd/mm/aaaa): ");
            DateOnly dataNascimento = EntradaDataNascimento();

            Console.Write("Informe o sexo: ");
            char sexo = EntradaSexo();

            Arquivo<Cliente> arquivo = new Arquivo<Cliente>(Constantes.DIRETORIO, Constantes.CLIENTE_ARQUIVO);

            arquivo.Inserir(new Cliente(cpf, nome, dataNascimento, sexo));
        }

    }
}
