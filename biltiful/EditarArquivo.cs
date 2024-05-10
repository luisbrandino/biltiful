
namespace biltiful
{
    internal class EditorArquivo
    {
        private readonly string CaminhoDiretorio = @"C:\Biltiful\";
        private readonly string CaminhoArquivo = "Venda.dat";

        protected EditorArquivo(string diretorio, string arquivo)
        {
            CaminhoDiretorio = diretorio;
            CaminhoArquivo = arquivo;

            if (!Directory.Exists(CaminhoDiretorio))
                Directory.CreateDirectory(CaminhoDiretorio);

            if (!File.Exists(CaminhoDiretorio + CaminhoArquivo))
            {
                var aux = File.Create(CaminhoDiretorio + CaminhoArquivo);
                aux.Close();
            }
        }

        protected List<string> Ler()
        {
            List<string> conteudo = new();

            foreach (string line in File.ReadAllLines(CaminhoDiretorio + CaminhoArquivo))
                conteudo.Add(line);

            return conteudo;
        }

        public void Escrever(List<string> conteudo)
        {
            File.WriteAllLines(CaminhoDiretorio + CaminhoArquivo, conteudo);
        }
    }
}
