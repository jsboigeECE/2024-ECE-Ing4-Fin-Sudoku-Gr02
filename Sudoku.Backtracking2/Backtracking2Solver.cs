using Sudoku.Shared;

namespace Sudoku.Backtracking2
{
    public class Backtracking2Solver : ISudokuSolver
    {
        public SudokuGrid Solve(SudokuGrid s)
        {
            int[,] sudoku;

            //Méthode pour utiliser un tableau format int[,] au lieu de [][] imposé
            //par le format de base
            //On créer donc un tableau int[,] qui prend toutes les valeurs de la
            //grille de sudoku en paramètre
            sudoku = Conversion(s);

            //Boucle pour mettre à jour le tableau du suduko à retourner à partir du
            //tableau sur lequel on a fait les modifications
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    s.Cells[i][j] = sudoku[i, j];

            return s;
        }
        public int[,] Conversion(SudokuGrid s)
        {

            int[,] sudok = new int[10, 10];

            //On remplace chaque case du nouveau tableau par la grille passée en
            //paramètre
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    sudok[i, j] = s.Cells[i][j];

            return sudok;
        }
    }
}
