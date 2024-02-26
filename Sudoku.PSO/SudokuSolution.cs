using System;
using System.IO;

namespace Sudoku.PSO
{
    // Nous encodons une solution du Sudoku sous la forme d'un vecteur d'entiers décrivant
    // l'ordre que la particule devrait suivre en visitant les grilles SudokuGrid
    public class SudokuSolution
    {
        public SudokuInstance Instance { get; protected set; }

        public int[] Path { get; protected set; }

        public SudokuSolution(SudokuInstance instance, int[] path)
        {
            Instance = instance;
            Path = path;
        }

        public void Write(string file)
        {
            double cost = SudokuUtils.Fitness(Instance, Path);

            using (StreamWriter writer = File.CreateText(file))
            {
                writer.WriteLine(cost);
                writer.WriteLine(Instance.NumberSudokuGrids);
                for (int i = 0; i < Instance.NumberSudokuGrids; i++)
                {
                    writer.WriteLine(Path[i] + 1);
                }
            }
        }
    }
}

