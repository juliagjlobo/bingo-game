using System;
using System.Collections.Generic;

public class Bingo
{
    const int num_card = 5; //define o tamanho da cartela
    const int num_total = 90; //define os números a serem sorteados

    static void Main(string[] args) //executa o loop principal de jogo
    {
        int[,] card = GenerateCard();
        int[] num_sorted = new int[num_total + 1];
        int win = 0;
        int draw;

        Random rand = new Random();
        List<int> num_available = new List<int>();
        for (int i = 1; i <= num_total; i++)
        {
            num_available.Add(i);
        }

        Console.WriteLine("### BINGO ###");
        DisplayCard(card, num_sorted);
        Console.WriteLine("< pressione qualquer tecla para iniciar o jogo >");
        Console.ReadKey();

        while (win == 0) //mantém o loop enquanto o jogador não tiver ganhado
        {
            draw = DrawNumber(num_available, rand);
            Console.WriteLine($"Foi sorteado o número: {draw}");

            num_sorted[draw] = 1;

            win = VerifyBingo(card, num_sorted);
            if (win == 1)
            {
                Console.WriteLine("Bingo!");
                Console.WriteLine("Trabalho 04 de Linguagens de Programação em 2024.1 por Júlia Lobo / RM: 24112475");
            }

            DisplayCard(card, num_sorted);
            Console.WriteLine("< pressione qualquer tecla para continuar >");
            Console.ReadKey();
        }
    }

    static int[,] GenerateCard() //gera uma cartela válida para o jogador
    {
        Random rand = new Random();
        int[,] card = new int[num_card, num_card];
        HashSet<int> usedNumbers = new HashSet<int>();

        for (int j = 0; j < num_card; j++)
        {
            List<int> columnNumbers = new List<int>();
            for (int i = 0; i < num_card; i++)
            {
                int number;
                do
                {
                    number = rand.Next(1, num_total + 1);
                } while (usedNumbers.Contains(number));
                usedNumbers.Add(number);
                columnNumbers.Add(number);
            }
            columnNumbers.Sort();
            for (int i = 0; i < num_card; i++)
            {
                card[i, j] = columnNumbers[i];
            }
        }

        return card;
    }

    static void DisplayCard(int[,] C, int[] s) //printa a cartela no console
    {
        for (int i = 0; i < num_card; i++)
        {
            for (int j = 0; j < num_card; j++)
            {
                if (s[C[i, j]] == 1)
                    Console.Write(" || ");
                else
                    Console.Write($" {C[i, j]:D2} ");
            }
            Console.WriteLine();
        }
    }

    static int VerifyBingo(int[,] C, int[] s) //verifica se houve bingo
    {
        for (int i = 0; i < num_card; i++)
        {
            if (VerifyVector(GetRow(C, i), s) == 1 || VerifyVector(GetColumn(C, i), s) == 1)
            {
                return 1;
            }
        }
        return 0;
    }

    static int VerifyVector(int[] v, int[] s) //verifica as colunas e linhas extraídas
    {
        for (int i = 0; i < num_card; i++)
        {
            if (s[v[i]] == 0)
                return 0;
        }
        return 1;
    }

    static int DrawNumber(List<int> num_available, Random rand) //sorteia números e retira os números sorteados do sorteio
    {
        if (num_available.Count == 0)
            throw new InvalidOperationException("Todos os números foram sorteados.");

        int index = rand.Next(num_available.Count);
        int num_drawn = num_available[index];
        num_available.RemoveAt(index);
        return num_drawn;
    }

    static int[] GetRow(int[,] C, int row) //extrai linhas individuais da cartela
    {
        int[] v = new int[num_card];
        for (int i = 0; i < num_card; i++)
        {
            v[i] = C[row, i];
        }
        return v;
    }

    static int[] GetColumn(int[,] C, int column) //extrai colunas individuais da cartela
    {
        int[] v = new int[num_card];
        for (int i = 0; i < num_card; i++)
        {
            v[i] = C[i, column];
        }
        return v;
    }
}
