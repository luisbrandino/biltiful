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
         *  Lê o arquivo retornando uma lista do tipo especificado, nulo se nada for encontrado no arquivo
         */
        public List<T>? Ler()
        {
            List<T>? entidades = null;

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

        }

        /**
         *  Insere uma nova entrada no arquivo, sem sobrescrever 
         */
        public void Inserir(T entidade)
        {
        }

    }
}
