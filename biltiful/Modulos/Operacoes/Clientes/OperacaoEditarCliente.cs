﻿using biltiful.Classes;
using biltiful.Modulos.Operacoes.Entradas;

namespace biltiful.Modulos.Operacoes
{
    internal class OperacaoEditarCliente
    {
        Arquivo<Cliente> arquivo;

        public OperacaoEditarCliente()
        {
            arquivo = new Arquivo<Cliente>(Constantes.DIRETORIO, Constantes.CLIENTE_ARQUIVO);
        }

        string EntradaCpf()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string? cpf) => Cliente.VerificarCPF(cpf),
                "CPF inválido"
            );

            /// Isso acaba por pesquisar o mesmo valor duas vezes no arquivo
            entrada.AdicionarRegra(
                (string? cpf) => arquivo.Ler().Find(c => c.CPF == cpf) != null,
                "CPF não encontrado"
            );

            return entrada.Pegar();
        }

        string EntradaNome()
        {
            Entrada<string> entrada = new();

            entrada.AdicionarRegra(
                (string? nome) => nome?.Length >= 1 && nome?.Length <= Constantes.TAMANHO_NOME_CLIENTE,
                "Nome precisa ter entre 1 e 50 caracteres"
            );

            return entrada.Pegar();
        }

        DateOnly EntradaDataNascimento()
        {
            EntradaData entrada = new();

            entrada.AdicionarRegra(
                (DateOnly data) => Cliente.VerificarDataDeNascimento(data),
                "Data não pode ser posterior à data atual"
            );

            return entrada.Pegar();
        }

        char EntradaSexo()
        {
            EntradaChar entrada = new();

            entrada.AdicionarRegra(
                (char sexo) => Cliente.VerificarSexo(sexo),
                "Sexo inválido. Uso: M/F"
            );

            return entrada.Pegar();
        }

        char EntradaSituacao()
        {
            EntradaChar entrada = new();

            entrada.AdicionarRegra(
                (char situacao) => char.ToUpper(situacao) == 'A' || char.ToUpper(situacao) == 'I',
                "Situação inválida. Uso: A/I"
            );

            return entrada.Pegar();
        }

        public void Executar()
        {
            Console.Clear();
            List<Cliente> clientes = arquivo.Ler();

            if (clientes.Count <= 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o CPF da pessoa a ser alterada: ");
            string cpf = EntradaCpf();
            
            Cliente? cliente = clientes.Find(c => c.CPF == cpf);

            Console.Write("Informe o nome: ");
            cliente.Nome = EntradaNome().ToUpper();

            Console.Write("Informe a data de nascimento: ");
            cliente.DataNascimento = EntradaDataNascimento();

            Console.Write("Informe o sexo (M/F): ");
            cliente.Sexo = EntradaSexo();

            Console.Write("Informe a situação (A/I): ");
            cliente.Situacao = EntradaSituacao();

            arquivo.Sobrescrever(clientes);

            Console.WriteLine("Cliente editado!");

            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
        }
    }
}
