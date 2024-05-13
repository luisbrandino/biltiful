using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Fornecedores
{
    internal class OperacaoCadastrarFornecedor
    {
        Arquivo<Fornecedor> arquivo;

        public OperacaoCadastrarFornecedor()
        {
            arquivo = new Arquivo<Fornecedor>(Constantes.DIRETORIO, Constantes.FORNECEDOR_ARQUIVO);
        }

        public string EntradaCnpj()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string cnpj) => Fornecedor.VerificarCNPJ(cnpj),
                "CNPJ inválido"
            );

            entrada.AdicionarRegra(
                (string cnpj) => arquivo.Ler().Find(f => f.CNPJ == cnpj) == null,
                "CNPJ já registrado"
            );

            return entrada.Pegar();
        }

        public string EntradaRazaoSocial()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string? razaoSocial) => razaoSocial.Length >= 1 && razaoSocial.Length <= Constantes.TAMANHO_NOME_FORNECEDOR,
                $"Nome precisa ter entre 1 e {Constantes.TAMANHO_NOME_FORNECEDOR} caracteres"
            );

            return entrada.Pegar();
        }

        public DateOnly EntradaDataAbertura()
        {
            EntradaData entrada = new();

            entrada.AdicionarRegra(
                (DateOnly data) => Fornecedor.VerificarDataAbertura(data),
                "Data de abertura não pode ser posterior à data atual"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            Console.Write("Informe o CNPJ: ");
            string cnpj = EntradaCnpj();

            Console.Write("Informe a razão social: ");
            string razaoSocial = EntradaRazaoSocial();

            Console.Write("Informe a data de abertura: ");
            DateOnly dataAbertura = EntradaDataAbertura();

            arquivo.Inserir(new Fornecedor(cnpj, razaoSocial, dataAbertura));

            Console.WriteLine("Fornecedor cadastrado!");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
        }

    }
}
