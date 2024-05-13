using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes.Bloqueados
{
    internal class OperacaoCadastroBloqueado
    {
        Arquivo<Bloqueado> arquivo;

        public OperacaoCadastroBloqueado()
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
                (string cnpj) => arquivo.Ler().Find(f => f.CNPJ == cnpj) == null,
                "CNPJ já registrado"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();
            Console.Write("Informe o CNPJ: ");
            string cnpj = EntradaCnpj();

            arquivo.Inserir(new Bloqueado(cnpj));

            Console.WriteLine("CNPJ bloqueado cadastrado!");

            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
        }
    }
}
