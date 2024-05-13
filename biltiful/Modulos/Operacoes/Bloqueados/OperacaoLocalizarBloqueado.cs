using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Bloqueados
{
    internal class OperacaoLocalizarBloqueado
    {
        Arquivo<Bloqueado> arquivo;

        public OperacaoLocalizarBloqueado()
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

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();
            Console.Write("Informe o CNPJ: ");
            string cnpj = EntradaCnpj();

            bool cnpjBloqueado = arquivo.Ler().Exists(f => f.CNPJ == cnpj);

            Console.WriteLine(cnpjBloqueado ? "CNPJ cadastrado como bloqueado" : "CNPJ não bloqueado");
            Console.ReadKey();
        }
    }
}
