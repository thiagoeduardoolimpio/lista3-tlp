using System;
using System.Collections.Generic;
using System.IO;
using CadastroEletroConsoleApp;

class Program
{
    static void mostrarEletro(List<Eletro> listaEletros)
    {
        for (int i = 0; i < listaEletros.Count; i++)
        {
            Console.WriteLine("Eletro " + (i + 1));
            Console.WriteLine($"Nome: {listaEletros[i].nome}");
            Console.WriteLine($"Potência (kW): {listaEletros[i].potencia}");
            Console.WriteLine($"Tempo de uso (horas/dia): {listaEletros[i].tempo}");
            Console.WriteLine("----------");
        }
    }

    static void addEletro(List<Eletro> listaEletros)
    {
        Eletro novoEletro = new Eletro();
        Console.WriteLine("*** Dados para o novo Eletro ***");
        Console.Write("Nome: ");
        novoEletro.nome = Console.ReadLine();
        Console.Write("Potência (kW): ");
        novoEletro.potencia = double.Parse(Console.ReadLine());
        Console.Write("Tempo de uso médio diário (horas): ");
        novoEletro.tempo = double.Parse(Console.ReadLine());
        listaEletros.Add(novoEletro);
    }

    static bool buscarEletro(List<Eletro> listaEletros, string nomeBusca)
    {
        bool encontrado = false;

        foreach (Eletro eletro in listaEletros)
        {
            if (eletro.nome.ToUpper().Contains(nomeBusca.ToUpper()))
            {
                mostrarUmEletro(eletro);
                encontrado = true;
            }
        }
        return encontrado;
    }

    static void mostrarUmEletro(Eletro eletro)
    {
        Console.WriteLine($"Nome: {eletro.nome}");
        Console.WriteLine($"Potência (kW): {eletro.potencia}");
        Console.WriteLine($"Tempo de uso: {eletro.tempo} horas/dia");
        Console.WriteLine($"Consumo diário: {eletro.potencia * eletro.tempo:F2} kWh");
        Console.WriteLine("----------");
    }

    static void buscarPorConsumo(List<Eletro> listaEletros, double limite)
    {
        bool encontrou = false;
        foreach (Eletro eletro in listaEletros)
        {
            if (eletro.potencia * eletro.tempo > limite)
            {
                mostrarUmEletro(eletro);
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine("Nenhum eletrodoméstico consome acima desse valor.");
        }
    }

    static void salvarDados(List<Eletro> listaEletros, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Eletro eletro in listaEletros)
        {
            writer.WriteLine($"{eletro.nome};{eletro.potencia};{eletro.tempo}");
        }
        writer.Dispose();
        Console.WriteLine("Dados salvos com sucesso!");
    }

    static void carregarDados(List<Eletro> listaEletros, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);

            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(';');

                if (campos.Length == 3)
                {
                    Eletro eletro = new Eletro();

                    eletro.nome = campos[0];
                    eletro.potencia = double.Parse(campos[1]);
                    eletro.tempo = double.Parse(campos[2]);

                    listaEletros.Add(eletro);
                }
            }
            Console.WriteLine("Dados carregados!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado.");
        }
    }

    static void calcularConsumo(List<Eletro> listaEletros)
    {
        Console.Write("Valor do kWh em R$: ");
        double valorKw = double.Parse(Console.ReadLine());
        double totalDia = 0;
        foreach (Eletro eletro in listaEletros)
        {
            totalDia += eletro.potencia * eletro.tempo;
        }
        double totalMes = totalDia * 30;
        Console.WriteLine($"Consumo diário: {totalDia:F2} kWh | R$ {totalDia * valorKw:F2}");
        Console.WriteLine($"Consumo mensal (30 dias): {totalMes:F2} kWh | R$ {totalMes * valorKw:F2}");
    }

    static int menu()
    {
        Console.WriteLine("=== CONTROLE DE CONSUMO DE ENERGIA RESIDENCIAL ===");
        Console.WriteLine("1 - Cadastrar Eletrodoméstico");
        Console.WriteLine("2 - Mostrar Eletrodomésticos");
        Console.WriteLine("3 - Buscar pelo Nome");
        Console.WriteLine("4 - Buscar consumo acima de X");
        Console.WriteLine("5 - Calcular consumo diário e mensal");
        Console.WriteLine("6 - Carregar dados");
        Console.WriteLine("0 - Sair e salvar");

        Console.Write("Escolha: ");
        return int.Parse(Console.ReadLine());
    }

    static void Main()
    {
        List<Eletro> listaEletros = new List<Eletro>();
        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    addEletro(listaEletros);
                    break;
                case 2:
                    mostrarEletro(listaEletros);
                    break;
                case 3:
                    Console.Write("Nome: ");

                    if (!buscarEletro(listaEletros, Console.ReadLine()))
                    {
                        Console.WriteLine("Eletrodoméstico não encontrado.");
                    }
                    break;
                case 4:
                    Console.Write("Limite de consumo diário em kWh: ");
                    buscarPorConsumo(listaEletros, double.Parse(Console.ReadLine()));
                    break;
                case 5:
                    calcularConsumo(listaEletros);
                    break;
                case 6:
                    carregarDados(listaEletros, "dadosEletros.txt");
                    break;
                case 0:
                    salvarDados(listaEletros, "dadosEletros.txt");
                    Console.WriteLine("Saindo...");
                    break;
            }
            if (opcao != 0)
            {
                Console.WriteLine("Pressione uma tecla...");
                Console.ReadKey();
                Console.Clear();
            }
        } while (opcao != 0);
    }
}
