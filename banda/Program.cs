using System;
using System.Collections.Generic;
using System.IO;
using Bandas;

class Program
{
    static void addBanda(List<Banda> listaBandas)
    {
        Banda novaBanda = new Banda();
        Console.WriteLine("Entre com os dados da Banda");
        Console.Write("Nome da banda: "); novaBanda.nome = Console.ReadLine();
        Console.Write("Gênero da banda: "); novaBanda.genero = Console.ReadLine();
        Console.Write("Integrantes da banda: "); novaBanda.integrantes = int.Parse(Console.ReadLine());
        Console.Write("Ranking da banda: "); novaBanda.ranking = int.Parse(Console.ReadLine());
        listaBandas.Add(novaBanda);
    }
    static void mostrarBandas(List<Banda> listaBandas)
    {
        Console.WriteLine("*** Bandas Cadastradas ***");
        for (int i = 0; i < listaBandas.Count; i++) mostrarDados(listaBandas[i]);
    }
    static void mostrarDados(Banda banda)
    {
        Console.WriteLine("Nome: " + banda.nome);
        Console.WriteLine("Gênero: " + banda.genero);
        Console.WriteLine("Integrantes: " + banda.integrantes);
        Console.WriteLine("Ranking: " + banda.ranking);
        Console.WriteLine("----------");
    }
    static void salvarDados(List<Banda> listaBandas, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Banda banda in listaBandas) writer.WriteLine($"{banda.nome};{banda.genero};{banda.integrantes};{banda.ranking}");
        writer.Dispose();
        Console.WriteLine("Dados salvos com sucesso!");
    }
    static void carregarDados(List<Banda> listaBandas, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(';');
                if (campos.Length == 4)
                {
                    Banda banda = new Banda(); banda.nome = campos[0]; banda.genero = campos[1];
                    banda.integrantes = int.Parse(campos[2]); banda.ranking = int.Parse(campos[3]);
                    listaBandas.Add(banda);
                }
            }
            Console.WriteLine("Dados carregados!");
        }
    }
    static bool buscarBanda(List<Banda> listaBandas, string nomeBanda)
    {
        bool encontrado = false;
        foreach (Banda banda in listaBandas)
            if (banda.nome.ToUpper().Contains(nomeBanda.ToUpper())) { mostrarDados(banda); encontrado = true; }
        return encontrado;
    }
    static int buscarIndiceBanda(List<Banda> listaBandas, string nomeBanda)
    {
        for (int i = 0; i < listaBandas.Count; i++)
            if (listaBandas[i].nome.ToUpper().Equals(nomeBanda.ToUpper())) return i;
        return -1;
    }
    static bool atualizarBanda(List<Banda> listaBandas, string nomeBanda)
    {
        int indice = buscarIndiceBanda(listaBandas, nomeBanda);
        if (indice == -1) return false;
        Console.Write("Novo Nome: "); listaBandas[indice].nome = Console.ReadLine();
        Console.Write("Novo Gênero: "); listaBandas[indice].genero = Console.ReadLine();
        Console.Write("Novos Integrantes: "); listaBandas[indice].integrantes = int.Parse(Console.ReadLine());
        Console.Write("Novo Ranking: "); listaBandas[indice].ranking = int.Parse(Console.ReadLine());
        return true;
    }
    static bool removerBanda(List<Banda> listaBandas, string nomeBanda)
    {
        int indice = buscarIndiceBanda(listaBandas, nomeBanda);
        if (indice == -1) return false;
        Console.Write("Confirma a remoção? (1-Sim | 2-Não): ");
        if (int.Parse(Console.ReadLine()) == 1) { listaBandas.RemoveAt(indice); return true; }
        return false;
    }
    static void buscarRanking(List<Banda> listaBandas, int ranking)
    {
        bool encontrou = false;
        foreach (Banda banda in listaBandas) if (banda.ranking == ranking) { mostrarDados(banda); encontrou = true; }
        if (!encontrou) Console.WriteLine("Nenhuma banda nesse ranking.");
    }
    static void buscarGenero(List<Banda> listaBandas, string genero)
    {
        bool encontrou = false;
        foreach (Banda banda in listaBandas) if (banda.genero.ToUpper().Contains(genero.ToUpper())) { mostrarDados(banda); encontrou = true; }
        if (!encontrou) Console.WriteLine("Nenhuma banda desse gênero.");
    }
    static int menu()
    {
        Console.WriteLine("=== SISTEMA DE CADASTRO DE BANDAS ===");
        Console.WriteLine("1 - Adicionar Banda\n2 - Mostrar Bandas\n3 - Buscar Banda\n4 - Atualizar Banda\n5 - Remover Banda\n6 - Buscar pelo Ranking\n7 - Buscar pelo Gênero\n8 - Carregar dados\n0 - Sair e salvar");
        Console.Write("Escolha: "); return int.Parse(Console.ReadLine());
    }
    static void Main()
    {
        List<Banda> listaBandas = new List<Banda>();
        int opcao;
        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1: addBanda(listaBandas); break;
                case 2: mostrarBandas(listaBandas); break;
                case 3: Console.Write("Nome da banda: "); if (!buscarBanda(listaBandas, Console.ReadLine())) Console.WriteLine("Banda não encontrada."); break;
                case 4: Console.Write("Banda que deseja atualizar: "); if (atualizarBanda(listaBandas, Console.ReadLine())) Console.WriteLine("Atualizada!"); else Console.WriteLine("Banda não encontrada."); break;
                case 5: Console.Write("Banda que deseja remover: "); if (removerBanda(listaBandas, Console.ReadLine())) Console.WriteLine("Removida!"); else Console.WriteLine("Banda não removida/encontrada."); break;
                case 6: Console.Write("Ranking: "); buscarRanking(listaBandas, int.Parse(Console.ReadLine())); break;
                case 7: Console.Write("Gênero: "); buscarGenero(listaBandas, Console.ReadLine()); break;
                case 8: carregarDados(listaBandas, "bandas.txt"); break;
                case 0: salvarDados(listaBandas, "bandas.txt"); Console.WriteLine("Saindo..."); break;
            }
            if (opcao != 0) { Console.WriteLine("Pressione uma tecla..."); Console.ReadKey(); Console.Clear(); }
        } while (opcao != 0);
    }
}
