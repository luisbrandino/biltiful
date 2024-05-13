using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Bloqueados
{
    internal class OperacaoRemoverBloqueado
    {
        Arquivo<Bloqueado> arquivo;

        public OperacaoRemoverBloqueado()
        {
            arquivo = new Arquivo<Bloqueado>(Constantes.DIRETORIO, Constantes.BLOQUEADO_ARQUIVO);
        }

        string EntradaCnpj()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string cnpj) => Fornecedor.VerificarCNPJ(cnpj),
                "CNPJ inválido"
            );

            entrada.AdicionarRegra(
                (string cnpj) => arquivo.Ler().Exists(f => f.CNPJ == cnpj),
                "CNPJ não registrado"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();

            List<Bloqueado> bloqueados = arquivo.Ler();

            if (bloqueados.Count <= 0)
            {
                Console.WriteLine("Não há CNPJs bloqueados cadastrados");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o CNPJ: ");
            string cnpj = EntradaCnpj();

            bloqueados.Remove(bloqueados.Find(f => f.CNPJ == cnpj));

            arquivo.Sobrescrever(bloqueados);

            Console.WriteLine("CNPJ removido!");
            Console.ReadKey();
        }
    }
}
