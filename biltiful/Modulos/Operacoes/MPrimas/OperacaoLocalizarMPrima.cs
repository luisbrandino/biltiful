using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.MPrimas
{
    internal class OperacaoLocalizarMPrima
    {
        Arquivo<MPrima> arquivo;

        public OperacaoLocalizarMPrima()
        {
            arquivo = new Arquivo<MPrima>(Constantes.DIRETORIO, Constantes.MPRIMA_ARQUIVO);
        }

        string EntradaId()
        {
            Entrada<string> entrada = new Entrada<string>();

            entrada.AdicionarRegra(
                (string id) => MPrima.VerificarId(id),
                "ID inválido. Formato: MP0000 (até MP9999)"
            );

            entrada.AdicionarRegra(
                (string id) => arquivo.Ler().Find(m => m.Id.ToUpper() == id.ToUpper() && m.Situacao == 'A') != null,
                "ID não cadastrado"
            );

            return entrada.Pegar().ToUpper();
        }

        public void Executar()
        {
            Console.Clear();

            List<MPrima> mPrimas = arquivo.Ler().Where(mPrima => mPrima.Situacao == 'A').ToList();

            if (mPrimas.Count == 0)
            {
                Console.WriteLine("Nenhuma matéria-prima registrada");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o ID: ");
            string id = EntradaId();

            MPrima? mPrima = mPrimas.Find(mp => mp.Id == id);

            Console.WriteLine(mPrima);

            Console.ReadKey();
        }
    }
}
