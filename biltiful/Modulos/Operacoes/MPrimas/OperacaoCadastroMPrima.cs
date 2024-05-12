using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.MPrimas
{
    internal class OperacaoCadastroMPrima
    {
        Arquivo<MPrima> arquivo;

        public OperacaoCadastroMPrima()
        {
            arquivo = new Arquivo<MPrima>(Constantes.DIRETORIO, Constantes.MPRIMA_ARQUIVO);
        }

        public string EntradaId()
        {
            Entrada<string> entrada = new Entrada<string>();

            entrada.AdicionarRegra(
                (string id) => MPrima.VerificarId(id),
                "ID inválido. Formato: MP0000 (até MP9999)"
            );

            entrada.AdicionarRegra(
                (string id) => arquivo.Ler().Find(m => m.Id == id) == null,
                "ID já cadastrado"
            );

            return entrada.Pegar();
        }

        public string EntradaNome()
        {
            Entrada<string> entrada = new Entrada<string>();

            entrada.AdicionarRegra(
                (string nome) => nome.Length >= 1 && nome.Length <= Constantes.TAMANHO_NOME_MPRIMA,
                $"Nome precisa ter entre 1 e {Constantes.TAMANHO_NOME_MPRIMA} caracteres"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            List<MPrima> mPrimas = arquivo.Ler();

            if (mPrimas.Count >= Constantes.LIMITE_CADASTRO_MPRIMAS)
            {
                Console.WriteLine("Não é possível cadastrar mais matérias-primas!");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o ID (MP0000-MP9999): ");
            string id = EntradaId();

            Console.Write("Informe o nome: ");
            string nome = EntradaNome();

            mPrimas.Add(new MPrima(id, nome));
            arquivo.Sobrescrever(mPrimas);

            Console.WriteLine("Matéria-prima cadastrada!");
            Console.ReadKey();
        }
    }
}
