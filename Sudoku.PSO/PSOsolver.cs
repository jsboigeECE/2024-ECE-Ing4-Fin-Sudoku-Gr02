using Sudoku.Shared;
using System;

namespace Sudoku.PSO
{
    public class PSOSolver : ISudokuSolver
    {
        public SudokuGrid Solve(SudokuGrid s)
        {
            var tableau = s.Cells; // Stocker les cellules de la grille de Sudoku

            // Créer un essaim de particules pour PSO
            int swarmSize = 50; // Taille de l'essaim
            Particle[] swarm = new Particle[swarmSize];
            SudokuGrid bestSolution = null;
            int maxIterations = 1000; // Nombre maximal d'itérations
            int iteration = 0;

            // Initialisation de l'essaim
            for (int i = 0; i < swarmSize; i++)
            {
                swarm[i] = new Particle(tableau); // Utiliser les cellules de la grille de Sudoku
            }

            while (iteration < maxIterations)
            {
                foreach (Particle particle in swarm)
                {
                    // Mettre à jour la position de la particule
                    particle.UpdatePosition();

                    // Vérifier si la nouvelle position est meilleure que la meilleure solution actuelle
                    if (particle.CurrentSolution.IsValid(s) && (bestSolution == null || particle.CurrentSolution.NbEmptyCells() < bestSolution.NbEmptyCells()))
                    {
                        bestSolution = particle.CurrentSolution.CloneSudoku();
                    }
                }

                iteration++;
            }

            return bestSolution;
        }
    }

    public class Particle
    {
        public SudokuGrid CurrentSolution { get; private set; }
        public SudokuGrid BestSolution { get; private set; }
        public SudokuGrid Velocity { get; private set; }

        public Particle(int[][] initialSolution)
        {
            CurrentSolution = new SudokuGrid { Cells = initialSolution };
            BestSolution = new SudokuGrid { Cells = initialSolution };
            Velocity = new SudokuGrid(); // Initialiser la vitesse
        }

        public void UpdatePosition()
        {
            // Générer une nouvelle solution basée sur la vitesse actuelle
            SudokuGrid newSolution = new SudokuGrid();
            Random random = new Random();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Mettre à jour chaque cellule en ajoutant la vitesse
                    int newValue = Math.Max(1, Math.Min(9, CurrentSolution.Cells[i][j] + Velocity.Cells[i][j]));

                    // Mettre à jour la nouvelle solution avec la nouvelle valeur
                    newSolution.Cells[i][j] = newValue;
                }
            }

            // Remplacer la solution actuelle par la nouvelle solution
            CurrentSolution = newSolution;
        }
        /* Met à jour la vitesse de la particule selon la formule PSO
        private void UpdateVelocity(Particle particle)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    double r1 = random.NextDouble();
                    double r2 = random.NextDouble();

                    int personalDifference = particle.PersonalBest.Cells[i][j] - particle.CurrentPosition.Cells[i][j];
                    int globalDifference = particle.PersonalBest.Cells[i][j] - particle.CurrentPosition.Cells[i][j];
                    
                    // Formule de mise à jour de la vitesse : V(t+1) = w * V(t) + c1 * rand() * (pbest - current) + c2 * rand() * (gbest - current)

                    int newVelocity = (int)(InertiaWeight * particle.Velocity.Cells[i][j] +
                                             C1 * r1 * personalDifference +
                                             C2 * r2 * globalDifference);

                    particle.Velocity.Cells[i][j] = newVelocity;
                }
            }*/

    }
}
