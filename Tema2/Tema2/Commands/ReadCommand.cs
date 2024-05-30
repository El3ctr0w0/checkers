using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Tema2.Commands
{
    public class ReadCommand
    {
        public static int[,] ReadMatrixFromFile(string filePath)
        {
            int[,] matrix = new int[8, 8];

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length ; i++)
            {
                string[] elements = lines[i].Split(' ');

                for (int j = 0; j < elements.Length -1 ; j++)
                {
                    matrix[i, j] = int.Parse(elements[j]);
                }
            }

            return matrix;
        }


        public static int ReadPlayerTurn(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int playerTurn = int.Parse(lines[0]);
            return playerTurn;
        }
        public static Tuple<int,int> ReadStats(string filePath)
        {
            int redWins=-1, maroonWins=-1;
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length > 0)
            {
                int.TryParse(lines[0], out redWins);
            }
            if (lines.Length > 1)
            {
                int.TryParse(lines[1], out maroonWins);
            }

            Tuple<int,int> tuple = new Tuple<int,int>(redWins, maroonWins);
            return tuple;
        }
        public void MakeMatrix(int[,] saveMatrix)
        {
            saveMatrix = ReadMatrixFromFile("C:\\Csharp\\SaveData.txt");
        }
    }
}
