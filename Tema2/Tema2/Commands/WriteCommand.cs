using System;
using System.IO;
using Tema2.View;
using Tema2.ViewModels;

namespace Tema2.Commands
{
    public class WriteCommand
    {
        public void SaveGame(string filePath, Piece[,] matrix, int playerTurn)
        {
            try
            {
                File.WriteAllText(filePath, string.Empty);

                using (StreamWriter writer = File.AppendText(filePath))
                {

                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] != null)
                            {
                                if (matrix[i, j].getColor() == 1)
                                {
                                    if (matrix[i, j].isItQueen())
                                    {
                                        writer.Write("3" + " ");
                                    }
                                    else writer.Write("1" + " ");
                                }
                                else if (matrix[i, j].getColor() == 2)
                                {
                                    writer.Write("2" + " ");
                                }
                                else writer.Write("4" + " ");
                            }
                            else writer.Write("0" + " ");
                        }
                        writer.WriteLine();
                    }
                }
                Console.WriteLine("Matricea a fost scrisă cu succes în fișierul: " + filePath);
                SavedPlayer(playerTurn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("A apărut o eroare la scrierea în fișier: " + ex.Message);
            }
        }
        public void SavedPlayer(int playerTurn)
        {
            string filePath = "C:\\Csharp\\PlayerTurn.txt";
            try
            {
                File.WriteAllText(filePath, string.Empty);

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.Write(playerTurn);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("A apărut o eroare la scrierea în fișier: " + ex.Message);
            }
        }

        public void StatsUpdate(int playerTurn)
        {
            string filePath = "C:\\Csharp\\Statistics.txt";
            (int red, int maroon) = Tema2.Commands.ReadCommand.ReadStats(filePath);
            if (playerTurn == 1)
            {
                red++;
            }
            else maroon++;

            try
            {
                File.WriteAllText(filePath, string.Empty);

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(red);
                    
                    writer.WriteLine(maroon);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("A apărut o eroare la scrierea în fișier: " + ex.Message);
            }

        }
    }
}
