using System;

namespace Sudoku.PSO
{
    public interface ITunableMetaheuristic : IMetaheuristic
    {
        void UpdateParameters(double[] parameters);
    }
}