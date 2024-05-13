using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Fornecedores
{
    internal class OperacaoLocalizarFornecedor
    {
        Arquivo<Fornecedor> arquivo;

        public OperacaoLocalizarFornecedor()
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
                (string cnpj) => arquivo.Ler().Find(f => f.CNPJ == cnpj && f.Situacao == 'A') != null,
                "CNPJ não registrado"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            List<Fornecedor> fornecedores = arquivo.Ler().Where(fornecedor => fornecedor.Situacao == 'A').ToList();

            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum produto registrado");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o CNPJ: ");
            string cnpj = EntradaCnpj();

            Fornecedor? fornecedor = fornecedores.Find(f => f.CNPJ == cnpj);

            Console.WriteLine(fornecedor);

            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
        }
    }
}
