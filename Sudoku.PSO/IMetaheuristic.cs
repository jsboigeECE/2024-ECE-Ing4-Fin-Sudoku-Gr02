// Decompiled with JetBrains decompiler
// Type: Metaheuristics.IMetaheuristic
// Assembly: Metaheuristic, Version=1.0.3825.24303, Culture=neutral, PublicKeyToken=null
// MVID: 66F6268B-01CE-4DA2-8B28-E75BE4A2A2B8
// Assembly location: C:\Users\talbi\RiderProjects\metaheuristics\Metaheuristic.dll

#nullable disable
namespace Sudoku.PSO
{
    public interface IMetaheuristic
    {
        string Name { get; }

        MetaheuristicType Type { get; }

        ProblemType Problem { get; }

        string[] Team { get; }

        void Start(string inputFile, string outputFile, int timeLimit);
    }
}