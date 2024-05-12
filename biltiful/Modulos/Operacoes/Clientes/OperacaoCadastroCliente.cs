using biltiful.Modulos.Operacoes.Entradas;
using biltiful.Classes;

namespace biltiful.Modulos.Operacoes
{
    internal class OperacaoCadastroCliente
    {
        Arquivo<Cliente> arquivo;

        public OperacaoCadastroCliente()
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

            entrada.AdicionarRegra(
                (string? cpf) => arquivo.Ler().Find(c => c.CPF == cpf) == null,
                "CPF já registrado"
            );

            return entrada.Pegar();
        }

        string EntradaNome()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string? nome) => nome?.Length >= 1 && nome?.Length <= Constantes.TAMANHO_NOME_CLIENTE,
                $"Nome precisa ter entre 1 e {Constantes.TAMANHO_NOME_CLIENTE} caracteres"
            );

            return entrada.Pegar();
        }

        DateOnly EntradaDataNascimento()
        {
            EntradaData entrada = new();

            entrada.AdicionarRegra(
                (DateOnly data) => Cliente.VerificarDataDeNascimento(data),
                "Data não pode ser posterior à data atual"
            );

            return entrada.Pegar();
        }

        char EntradaSexo()
        {
            EntradaChar entrada = new();

            entrada.AdicionarRegra(
                (char sexo) => Cliente.VerificarSexo(sexo),
                "Sexo inválido. Uso: M/F"    
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            Console.Write("Informe o CPF: ");
            string cpf = EntradaCpf();

            Console.Write("Informe o nome: ");
            string nome = EntradaNome();

            Console.Write("Digite a data de nascimento (dd/mm/aaaa): ");
            DateOnly dataNascimento = EntradaDataNascimento();

            Console.Write("Informe o sexo: ");
            char sexo = EntradaSexo();

            arquivo.Inserir(new Cliente(cpf, nome, dataNascimento, sexo));

            Console.WriteLine("Usuário cadastrado!");

            Console.ReadKey();
        }

    }
}
