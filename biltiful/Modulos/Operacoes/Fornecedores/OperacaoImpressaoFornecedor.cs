using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Fornecedores
{
    internal class OperacaoImpressaoFornecedor
    {
        Arquivo<Fornecedor> arquivo;

        public OperacaoImpressaoFornecedor()
        {
            arquivo = new Arquivo<Fornecedor>(Constantes.DIRETORIO, Constantes.FORNECEDOR_ARQUIVO);
        }

        public void Executar()
        {
            Console.Clear();
            List<Fornecedor> fornecedores = arquivo.Ler().Where(fornecedor => fornecedor.Situacao == 'A').ToList();

            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Não há fornecedores cadastrados!");
                Console.ReadKey();

                return;
            }

            new Navegador<Fornecedor>(fornecedores).Iniciar();
        }
    }
}
