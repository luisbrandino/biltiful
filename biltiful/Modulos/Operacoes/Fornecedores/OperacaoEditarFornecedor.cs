using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Fornecedores
{
    internal class OperacaoEditarFornecedor
    {
        Arquivo<Fornecedor> arquivo;

        public OperacaoEditarFornecedor()
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
                (string cnpj) => arquivo.Ler().Find(f => f.CNPJ == cnpj) != null,
                "CNPJ não registrado"
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

        char EntradaSituacao()
        {
            EntradaChar entrada = new();

            entrada.AdicionarRegra(
                (char situacao) => char.ToUpper(situacao) == 'A' || char.ToUpper(situacao) == 'I',
                "Situação inválida. Uso: A/I"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            List<Fornecedor> fornecedores = arquivo.Ler();

            if (fornecedores.Count <= 0)
            {
                Console.WriteLine("Nenhum fornecedor cadastrado");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o CNPJ: ");
            string cnpj = EntradaCnpj();

            Fornecedor fornecedor = fornecedores.Find(f => f.CNPJ == cnpj);

            Console.Write("Informe a razão social: ");
            fornecedor.RazaoSocial = EntradaRazaoSocial();

            Console.Write("Informe a data de abertura: ");
            fornecedor.DataAbertura = EntradaDataAbertura();

            Console.Write("Informe a situação (A/I): ");
            fornecedor.Situacao = EntradaSituacao();

            arquivo.Sobrescrever(fornecedores);

            Console.WriteLine("Fornecedor alterado!");
            Console.ReadKey();
        }

    }
}
