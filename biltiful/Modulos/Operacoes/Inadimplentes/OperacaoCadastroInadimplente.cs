using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Inadimplentes
{
    internal class OperacaoCadastroInadimplente
    {
        Arquivo<Inadimplente> arquivo;

        public OperacaoCadastroInadimplente()
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
                (string? cpf) => arquivo.Ler().Find(c => c.CPF == cpf) == null,
                "CPF já registrado"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();
            Console.Write("Informe o CPF: ");
            string cpf = EntradaCpf();

            arquivo.Inserir(new Inadimplente(cpf));

            Console.WriteLine("Inadimplente cadastrado!");

            Console.ReadKey();
        }
    }
}
