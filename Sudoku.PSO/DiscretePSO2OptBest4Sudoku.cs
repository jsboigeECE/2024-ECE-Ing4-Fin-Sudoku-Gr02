using Sudoku.PSO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sudoku.PSO
{
    public class DiscretePSO2OptBest4Sudoku : DiscretePSO
    {
        public SudokuInstance Instance { get; protected set; }

        protected int generatedSolutions;

        public DiscretePSO2OptBest4Sudoku(SudokuInstance instance, int partsCount, double prevConf,
                                double neighConf, int[] lowerBounds, int[] upperBounds)
            : base(partsCount, prevConf, neighConf, lowerBounds, upperBounds)
        {
            Instance = instance;
            LocalSearchEnabled = true;
            generatedSolutions = 0;
        }

        protected override double Fitness(int[] individual)
        {
            return SudokuUtils.Fitness(Instance, individual);
        }

        protected override int[] InitialSolution()
        {
            int[] solution;

            if (generatedSolutions == 0)
            {
                solution = SudokuUtils.GreedySolution(Instance);
            }
            else
            {
                solution = SudokuUtils.RandomSolution(Instance);
            }

            generatedSolutions++;
            return solution;
        }


        protected override void LocalSearch(int[] solution)
        {
            SudokuUtils.LocalSearch2OptBest(Instance, solution);
        }
    }
}
