﻿namespace biltiful.Entidades
{
    internal class Cliente
    {
        public string CPF { get; private set; }
        public string Nome { get; set; }
        public DateOnly DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateOnly UltimaCompra {  get; set; }
        public DateOnly DataCadastro { get; set; }
        public char Situacao { get; set; }

        /**
         *  Esse construtor serve para carregar um objeto Cliente com a linha vinda do arquivo referente
         */
        public Cliente(string dados)
        {
            if (dados.Length != 87)
                throw new ArgumentException("Linha não possui o tamanho padrão para a entidade Cliente");

            CPF = dados.Substring(0, 10);
            Nome = dados.Substring(11, 50);

            int dia = int.Parse(dados.Substring(61, 2));
            int mes = int.Parse(dados.Substring(63, 2));
            int ano = int.Parse(dados.Substring(65, 4));

            DataNascimento = new DateOnly(ano, mes, dia);

            Sexo = dados.Substring(69, 1).First();

            dia = int.Parse(dados.Substring(70, 2));
            mes = int.Parse(dados.Substring(72, 2));
            ano = int.Parse(dados.Substring(74, 4));

            UltimaCompra = new DateOnly(ano, mes, dia);

            dia = int.Parse(dados.Substring(78, 2));
            mes = int.Parse(dados.Substring(80, 2));
            ano = int.Parse(dados.Substring(82, 4));

            DataCadastro = new DateOnly(ano, mes, dia);

            Situacao = dados.Substring(86, 1).First();
        }

        /**
         *  Esse construtor cria o objeto com suas informações sendo passadas diretamente
         */
        public Cliente(string cpf, string nome, string sexo, DateOnly dataCadastro)
        {

        }

        /**
         *  Esse método verifica se o CPF é válido
         */
        public bool ValidarCPF(string cpf)
        {
            return true;
        }

        /**
         *  Esse método transforma o objeto atual em uma linha que pode ser escrita
         */
        public string FormatarParaArquivo()
        {
            return string.Empty;
        }
    }
}
