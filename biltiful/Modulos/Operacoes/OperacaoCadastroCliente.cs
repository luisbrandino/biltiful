using biltiful.Classes;

namespace biltiful.Modulos.Operacoes
{
    internal class OperacaoCadastroCliente
    {

        public void Executar()
        {
            Console.Clear();

            Console.Write("Informe o CPF: ");
            string cpf = Console.ReadLine();

            while (!Cliente.VerificarCPF(cpf))
            {
                Console.Write("CPF inválido, por favor tente novamente: ");
                cpf = Console.ReadLine();
            }

            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine().Trim().ToUpper();

            if (nome.Length > 50)
                nome = nome.Substring(0, Constantes.TAMANHO_NOME_CLIENTE);

            Console.Write("Digite a data de nascimento (dd/mm/aaaa): ");
            DateOnly dataNascimento;

            while (true)
            {
                try
                {
                    dataNascimento = DateOnly.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Write("Data de nascimento inválida, tente novamente: ");
                    continue;
                }

                if (!Cliente.VerificarDataDeNascimento(dataNascimento))
                {
                    Console.Write("Data nascimento não pode ser posterior à data atual, tente novamente: ");
                    continue;
                }

                break;
            }

            Console.Write("Informe o sexo: ");
            char sexo;
            while (true)
            {
                sexo = Console.ReadLine().First();

                if (Cliente.VerificarSexo(sexo))
                    break;

                Console.Write("Sexo inválido, tente novamente: ");
            }

            Arquivo<Cliente> arquivo = new Arquivo<Cliente>(Constantes.DIRETORIO, Constantes.CLIENTE_ARQUIVO);

            arquivo.Inserir(new Cliente(cpf, nome, dataNascimento, sexo));
        }

    }
}
