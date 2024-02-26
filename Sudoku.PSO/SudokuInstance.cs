using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Sudoku.PSO
{
    public class SudokuInstance
    {
        public int NumberSudokuGrids { get; protected set; }
        public double[,] NbErrors { get; protected set; }

        public SudokuInstance(string file)
        {
            Regex regex = new Regex(@"\s+");
            double[] xCoords = null, yCoords = null;

            using (StreamReader reader = File.OpenText(file))
            {
                string line = "";
                NumberSudokuGrids = -1;
                while (NumberSudokuGrids == -1)
                {
                    line = reader.ReadLine();
                    if (line.StartsWith("DIMENSION"))
                    {
                        NumberSudokuGrids = int.Parse(line.Substring(11));
                        xCoords = new double[NumberSudokuGrids];
                        yCoords = new double[NumberSudokuGrids];
                        NbErrors = new double[NumberSudokuGrids, NumberSudokuGrids];
                    }
                }

                while (!line.StartsWith("NODE_COORD_SECTION"))
                {
                    line = reader.ReadLine();
                }
                for (int k = 0; k < NumberSudokuGrids; k++)
                {
                    line = reader.ReadLine();
                    string[] parts = regex.Split(line.Trim());
                    int i = int.Parse(parts[0]) - 1;
                    xCoords[i] = double.Parse(parts[1]);
                    yCoords[i] = double.Parse(parts[2]);
                }
            }

            for (int i = 0; i < NumberSudokuGrids; i++)
            {
                for (int j = 0; j < NumberSudokuGrids; j++)
                {
                    NbErrors[i, j] = Math.Sqrt(Math.Pow(xCoords[i] - xCoords[j], 2) + Math.Pow(yCoords[i] - yCoords[j], 2));
                }
            }
        }
    }
}