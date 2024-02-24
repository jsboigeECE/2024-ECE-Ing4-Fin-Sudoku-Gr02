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
        static bool IsSafe(int[,] grid, int row, int col, int num)
        {
            for (int x = 0; x <= 8; x++)
                if (grid[row, x] == num)
                    return false;

            for (int x = 0; x <= 8; x++)
                if (grid[x, col] == num)
                    return false;

            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[i + startRow, j + startCol] == num)
                        return false;

            return true;
        }
    }
}
