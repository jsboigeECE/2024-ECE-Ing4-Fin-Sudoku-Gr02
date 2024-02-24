using Sudoku.PSO;
using Sudoku.Shared;
using System;

namespace Sudoku.PSO
{
    public class SolverPSO : ISudokuSolver
    {
        private Random _rnd;

        public SudokuGrid Solve(SudokuGrid s)
        {
            // Initialisation des paramètres
            const int numOrganisms = 200; // nombre d'organisme
            const int maxEpochs = 5000; // nombre max d'époques
            const int maxRestarts = 20; // nombre max de restarts

            // Affichage des paramètres
            Console.WriteLine("Nombre d'organismes défini à : " + numOrganisms);
            Console.WriteLine("Nombre maximal d'époques défini à : " + maxEpochs);
            Console.WriteLine("Nombre maximal de redémarrages défini à : " + maxRestarts);

            // Conversion de la grille SudokuGrid en Sudoku
            int[,] CellsSolver = new int[9, 9]; // Instanciation d'un tabeau bidimensionnel d'entier, donc ici un tableau de dim 9x9
            
            // Double boucle pour remplir
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    CellsSolver[i, j] = s.Cells[i][j];
                }
            }

            // Création du nouveau Sudoku à partir de la grille SudokuGrid
            var sudoku = new Sudoku(CellsSolver);

            // Résolution du Sudoku
            var solvedSudoku = Solve(sudoku, numOrganisms, maxEpochs, maxRestarts);

            // Conversion inverse du Sudoku pour récupérer la grille SudokuGrid
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    s.Cells[i][j] = solvedSudoku.CellValues[i, j];
                }
            }

            // Sudoku au format d'une grille SudokuGrid
            return s;

        }

        public Sudoku Solve(Sudoku sudoku, int numOrganisms, int maxEpochs, int maxRestarts)
        {
            var error = int.MaxValue;
            Sudoku bestSolution = null;
            var attempt = 0;
            while (error != 0 && attempt < maxRestarts)
            {
                Console.WriteLine($"Attempt: {attempt}");
                _rnd = new Random(attempt);
                bestSolution = SolveInternal(sudoku, numOrganisms, maxEpochs);
                error = bestSolution.Error;
                ++attempt;
            }

            return bestSolution;
        }

        private Sudoku SolveInternal(Sudoku sudoku, int numOrganisms, int maxEpochs)
        {
            var numberOfWorkers = (int)(numOrganisms * 0.90);
            var hive = new Organism[numOrganisms];

            var bestError = int.MaxValue;
            Sudoku bestSolution = null;

            for (var i = 0; i < numOrganisms; ++i)
            {
                var organismType = i < numberOfWorkers
                  ? OrganismType.Worker
                  : OrganismType.Explorer;

                var randomSudoku = Sudoku.New(MatrixHelper.RandomMatrix(_rnd, sudoku.CellValues));
                var err = randomSudoku.Error;

                hive[i] = new Organism(organismType, randomSudoku.CellValues, err, 0);

                if (err >= bestError) continue;
                bestError = err;
                bestSolution = Sudoku.New(randomSudoku);
            }

            var epoch = 0;
            while (epoch < maxEpochs)
            {
                if (epoch % 1000 == 0)
                    Console.WriteLine($"Epoch: {epoch}, Best error: {bestError}");

                if (bestError == 0)
                    break;

                for (var i = 0; i < numOrganisms; ++i)
                {
                    if (hive[i].Type == OrganismType.Worker)
                    {
                        var neighbor = MatrixHelper.NeighborMatrix(_rnd, sudoku.CellValues, hive[i].Matrix);
                        var neighborSudoku = Sudoku.New(neighbor);
                        var neighborError = neighborSudoku.Error;

                        var p = _rnd.NextDouble();
                        if (neighborError < hive[i].Error || p < 0.001)
                        {
                            hive[i].Matrix = MatrixHelper.DuplicateMatrix(neighbor);
                            hive[i].Error = neighborError;
                            if (neighborError < hive[i].Error) hive[i].Age = 0;

                            if (neighborError >= bestError) continue;
                            bestError = neighborError;
                            bestSolution = neighborSudoku;
                        }
                        else
                        {
                            hive[i].Age++;
                            if (hive[i].Age <= 1000) continue;
                            var randomSudoku = Sudoku.New(MatrixHelper.RandomMatrix(_rnd, sudoku.CellValues));
                            hive[i] = new Organism(0, randomSudoku.CellValues, randomSudoku.Error, 0);
                        }
                    }
                    else
                    {
                        var randomSudoku = Sudoku.New(MatrixHelper.RandomMatrix(_rnd, sudoku.CellValues));
                        hive[i].Matrix = MatrixHelper.DuplicateMatrix(randomSudoku.CellValues);
                        hive[i].Error = randomSudoku.Error;

                        if (hive[i].Error >= bestError) continue;
                        bestError = hive[i].Error;
                        bestSolution = randomSudoku;
                    }
                }

                // merge best worker with best explorer into worst worker
                var bestWorkerIndex = 0;
                var smallestWorkerError = hive[0].Error;
                for (var i = 0; i < numberOfWorkers; ++i)
                {
                    if (hive[i].Error >= smallestWorkerError) continue;
                    smallestWorkerError = hive[i].Error;
                    bestWorkerIndex = i;
                }

                var bestExplorerIndex = numberOfWorkers;
                var smallestExplorerError = hive[numberOfWorkers].Error;
                for (var i = numberOfWorkers; i < numOrganisms; ++i)
                {
                    if (hive[i].Error >= smallestExplorerError) continue;
                    smallestExplorerError = hive[i].Error;
                    bestExplorerIndex = i;
                }

                var worstWorkerIndex = 0;
                var largestWorkerError = hive[0].Error;
                for (var i = 0; i < numberOfWorkers; ++i)
                {
                    if (hive[i].Error <= largestWorkerError) continue;
                    largestWorkerError = hive[i].Error;
                    worstWorkerIndex = i;
                }

                var merged = MatrixHelper.MergeMatrices(_rnd, hive[bestWorkerIndex].Matrix, hive[bestExplorerIndex].Matrix);
                var mergedSudoku = Sudoku.New(merged);

                hive[worstWorkerIndex] = new Organism(0, merged, mergedSudoku.Error, 0);
                if (hive[worstWorkerIndex].Error < bestError)
                {
                    bestError = hive[worstWorkerIndex].Error;
                    bestSolution = mergedSudoku;
                }

                ++epoch;
            }

            return bestSolution;
        }
    }
}