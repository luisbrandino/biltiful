using biltiful.Classes;

namespace biltiful.Modulos.Operacoes
{
    internal class OperacaoImpressaoCliente
    {
        Arquivo<Cliente> arquivo;

        public OperacaoImpressaoCliente()
        {
            arquivo = new Arquivo<Cliente>(Constantes.DIRETORIO, Constantes.CLIENTE_ARQUIVO);   
        }

        public void Executar()
        {
            Console.Clear();
            List<Cliente> clientes = arquivo.Ler().Where(cliente => cliente.Situacao == 'A').ToList();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há clientes cadastrados!");
                Console.ReadKey();

                return;
            }

            new Navegador<Cliente>(clientes).Iniciar();
        }

    }
}
