using Sudoku.PSO;
using System;
using System.Collections;
using System.Collections.Generic;
using Sudoku.Shared;

namespace Sudoku.PSO
{
    public static class SudokuUtils
    {
        public static  SudokuGrid ConvertToSudoku(int[] path)
        {
			throw new NotImplementedException();
		}

		public static int[] ConvertFromSudoku(SudokuGrid sudoku)
		{
			throw new NotImplementedException();
		}




		public static double Fitness(SudokuGrid instance, int[] path)
        {
            var targetSudoku = ConvertToSudoku(path);
            return - targetSudoku.NbErrors(instance);
        }

        public static SudokuGrid RandomSolution(SudokuGrid instance)
        {
            var random = new Random();
            var grid = new SudokuGrid();
            var cells = new int[9][];
            for (var i = 0; i < 9; i++)
            {
                cells[i] = new int[9];
                for (var j = 0; j < 9; j++)
                {
                    cells[i][j] = instance.Cells[i][j];
                }
            }

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    if (cells[i][j] == 0)
                    {
                        var possibleValues = instance.GetAvailableNumbers(i, j);
                        cells[i][j] = possibleValues[random.Next(possibleValues.Length)];
                    }
                }
            }
            grid.Cells = cells;
            return grid;
        }

        // Implementation of the 2-opt (best improvement) local search algorithm.
        public static void LocalSearch2OptBest(SudokuGrid instance, int[] path)
        {
            int tmp;
            int firstSwapItem = 0, secondSwapItem = 0;
            double currentFitness, bestFitness;

            bestFitness = Fitness(instance, path);
            for (int j = 1; j < path.Length; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    // Swap the items.
                    tmp = path[j];
                    path[j] = path[i];
                    path[i] = tmp;

                    // Evaluate the fitness of this new solution.
                    currentFitness = Fitness(instance, path);
                    if (currentFitness < bestFitness)
                    {
                        firstSwapItem = j;
                        secondSwapItem = i;
                        bestFitness = currentFitness;
                    }

                    // Undo the swap.
                    tmp = path[j];
                    path[j] = path[i];
                    path[i] = tmp;
                }
            }

            // Use the best assignment.
            if (firstSwapItem != secondSwapItem)
            {
                tmp = path[firstSwapItem];
                path[firstSwapItem] = path[secondSwapItem];
                path[secondSwapItem] = tmp;
            }
        }

        // Implementation of the Tabu Movement of two movements.
        public static Tuple<int, int> GetTabu(int[] source, int[] destiny)
        {
            Tuple<int, int> tabu = new Tuple<int, int>(-1, -1);

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != destiny[i])
                {
                    tabu.Val1 = Math.Min(source[i], destiny[i]);
                    tabu.Val2 = Math.Max(source[i], destiny[i]);
                    break;
                }
            }

            return tabu;
        }
    }
}

