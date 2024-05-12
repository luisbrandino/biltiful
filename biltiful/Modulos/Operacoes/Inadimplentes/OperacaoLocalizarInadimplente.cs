using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Inadimplentes
{
    internal class OperacaoLocalizarInadimplente
    {
        Arquivo<Inadimplente> arquivo;

        public OperacaoLocalizarInadimplente()
        {
            arquivo = new Arquivo<Inadimplente>(Constantes.DIRETORIO, Constantes.INADIMPLENTE_ARQUIVO);
        }

        string EntradaCpf()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string cpf) => Cliente.VerificarCPF(cpf),
                "CPF inválido"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();
            Console.Write("Informe o CPF: ");
            string cpf = EntradaCpf();

            bool cpfInadimplente = arquivo.Ler().Exists(c => c.CPF == cpf);

            Console.WriteLine(cpfInadimplente ? "CPF cadastrado como inadimplente" : "CPF não inadimplente");
            Console.ReadKey();
        }
    }
}
