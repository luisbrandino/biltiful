namespace biltiful.Modulos
{
    internal class ModuloCadastro
    {

        public void Executar()
        {
            Menu menu = new Menu("Clientes", "Produtos", "Matérias-prima", "Fornecedores", "Inadimplentes", "Bloqueados");

            menu.DefinirTitulo("MENU DE CADASTROS");

            menu.Perguntar();
        }

    }
}
