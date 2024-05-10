using biltiful.Classes;

namespace biltiful
{
    internal class Arquivo<T> where T : IEntidade, new()
    {
        public string Caminho { get; private set; }

        /**
         *  Construtor recebe o caminho final apontado para o arquivo diretamente
         */
        public Arquivo(string caminhoFinal)
        {
            Caminho = caminhoFinal;
        }

        /**
         *  Construtor recebe o caminho junto do arquivo a ser apontado
         */
        public Arquivo(string caminho, string arquivo)
        {
            Caminho = caminho + arquivo;
        }

        /**
         *  Lê o arquivo retornando uma lista do tipo especificado
         */
        public List<T> Ler()
        {
            List<T> entidades = new List<T>();

            string[] linhas = File.ReadAllLines(Caminho);

            foreach (string linha in linhas)
            {
                try
                {
                    T entidade = new T();
                    entidade.LinhaParaObjeto(linha);

                    entidades.Add(entidade);
                } catch (Exception)
                {
                }
            }

            return entidades;
        }

        /**
         *  Sobrescreve o arquivo atual com a nova lista do tipo especificado passado no parâmetro
         */
        public void Sobrescrever(List<T> entidades)
        {
            StreamWriter escritor = new StreamWriter(Caminho);

            foreach (T entidade in entidades)
                escritor.WriteLine(entidade.FormatarParaArquivo());

            escritor.Close();
        }

        /**
         *  Insere uma nova entrada no arquivo. Essencialmente é apenas um atalho, já que utiliza os métodos
         *  Ler e Sobrescrever.
         */
        public void Inserir(T entidade)
        {
            List<T> entidades = Ler();

            entidades.Add(entidade);

            Sobrescrever(entidades);
        }

    }
}
