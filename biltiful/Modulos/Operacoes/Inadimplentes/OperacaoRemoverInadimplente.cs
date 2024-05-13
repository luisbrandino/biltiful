using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Inadimplentes
{
    internal class OperacaoRemoverInadimplente
    {
        Arquivo<Inadimplente> arquivo;

        public OperacaoRemoverInadimplente()
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

            entrada.AdicionarRegra(
                (string cpf) => arquivo.Ler().Exists(c => c.CPF == cpf),
                "CPF não registrado"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            List<Inadimplente> inadimplentes = arquivo.Ler();

            if (inadimplentes.Count <= 0)
            {
                Console.WriteLine("Não há inadimplentes cadastrados");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o CPF: ");
            string cpf = EntradaCpf();

            inadimplentes.Remove(inadimplentes.Find(c => c.CPF == cpf));

            arquivo.Sobrescrever(inadimplentes);

            Console.WriteLine("CPF removido!");
            Console.ReadKey();
        }
    }
}
