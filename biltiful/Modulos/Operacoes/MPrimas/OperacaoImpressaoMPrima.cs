using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.MPrimas
{
    internal class OperacaoImpressaoMPrima
    {
        Arquivo<MPrima> arquivo;

        public OperacaoImpressaoMPrima()
        {
            arquivo = new Arquivo<MPrima>(Constantes.DIRETORIO, Constantes.MPRIMA_ARQUIVO);
        }

        public void Executar()
        {
            Console.Clear();
            List<MPrima> mPrimas = arquivo.Ler().Where(mPrima => mPrima.Situacao == 'A').ToList();

            if (mPrimas.Count == 0)
            {
                Console.WriteLine("Não há fornecedores cadastrados!");
                Console.ReadKey();

                return;
            }

            new Navegador<MPrima>(mPrimas).Iniciar();
        }
    }
}
