using biltiful;
using biltiful.Modulos;

// menu padrão
Menu menu = new Menu("Módulo de cadastro", "Módulo de vendas", "Módulo de compra de matéria-prima", "Módulo de produção de cosméticos");

menu.DefinirTitulo(">> MÓDULOS <<");
menu.LimparAposImpressao(true);

while (true)
{
    switch (menu.Perguntar())
    {
        case 1:
            new ModuloCadastro().Executar();
            break;
        case 2:
            new ModuloVenda().Executar();
            break;
        case 3:
            new ModuloCosmetico().Executar();
            break;
        case 4:
            new ModuloProducao().Executar();
            break;
        default:
            Environment.Exit(0);
            break;
    }
}