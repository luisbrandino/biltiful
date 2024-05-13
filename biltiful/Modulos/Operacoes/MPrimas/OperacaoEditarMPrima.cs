using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.MPrimas
{
    internal class OperacaoEditarMPrima
    {
        Arquivo<MPrima> arquivo;

        public OperacaoEditarMPrima()
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
                (string id) => arquivo.Ler().Find(m => m.Id.ToUpper() == id.ToUpper()) != null,
                "ID não cadastrado"
            );

            return entrada.Pegar().ToUpper();
        }

        string EntradaNome()
        {
            Entrada<string> entrada = new Entrada<string>();

            entrada.AdicionarRegra(
                (string nome) => nome.Length >= 1 && nome.Length <= Constantes.TAMANHO_NOME_MPRIMA,
                $"Nome precisa ter entre 1 e {Constantes.TAMANHO_NOME_MPRIMA} caracteres"
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
            List<MPrima> mPrimas = arquivo.Ler();

            if (mPrimas.Count <= 0)
            {
                Console.WriteLine("Nenhuma matéria-prima cadastrada");
                Console.WriteLine("Aperte qualquer tecla para continuar");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o ID: ");
            string id = EntradaId();

            MPrima mPrima = mPrimas.Find(mp => mp.Id == id);

            Console.Write("Informe o nome: ");
            mPrima.Nome = EntradaNome();

            Console.Write("Informe a situação (A/I): ");
            mPrima.Situacao = EntradaSituacao();

            arquivo.Sobrescrever(mPrimas);

            Console.WriteLine("Matéria-prima alterada!");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
        }
    }
}
