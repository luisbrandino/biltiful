/**
 *  Interface necessária para que a classe Arquivo consiga manipular as outras classes e instanciá-las
 *  
 *  Essa interface só garante que todas as classes vão ter os métodos LinhaParaObjeto() e FormatarParaArquivo(),
 *  permitindo, desse modo, que a classe Arquivo crie elas só com a linha vinda do arquivo.
 */
namespace biltiful.Classes
{
    internal interface IEntidade
    {
        void LinhaParaObjeto(string linha);

        string FormatarParaArquivo();
    }
}
