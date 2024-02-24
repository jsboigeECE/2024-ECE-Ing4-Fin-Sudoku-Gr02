static void Main(string[] args)
{
    // Création d'une grille de Sudoku vide ou avec un puzzle prédéfini.
    GrilleSudoku grille = new GrilleSudoku();
    // TODO: Initialiser la grille avec des valeurs ou lire à partir d'une source.

    // Création et utilisation du solveur.
    SolverHumain solveur = new SolverHumain();
    solveur.Solve(grille);

    // Affichage de la grille résolue.
    for (int i = 0; i < 9; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            // Remplacer GetCellule avec la méthode appropriée pour obtenir la valeur de la cellule.
            Console.Write(grille.GetCellule(j, i) + " ");
        }
        Console.WriteLine();
    }
}

