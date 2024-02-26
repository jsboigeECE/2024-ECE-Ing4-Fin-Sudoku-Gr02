﻿using Python.Runtime;
using Sudoku.Shared;

namespace Sudoku.SimulatedAnnealing;

public class SimulatedAnnealingSolver: PythonSolverBase

	{
		public override Shared.SudokuGrid Solve(Shared.SudokuGrid s)
		{
			//System.Diagnostics.Debugger.Break();

			//For some reason, the Benchmark runner won't manage to get the mutex whereas individual execution doesn't cause issues
			//using (Py.GIL())
			//{
			// create a Python scope
			using (PyModule scope = Py.CreateScope())
			{
				// convert the Cells array object to a PyObject
				PyObject pyCells = s.Cells.ToPython();

				// create a Python variable "instance"
				scope.Set("instance", pyCells);

				// run the Python script
				//string code = File.ReadAllText(@"C:\Users\simon\Desktop\Ecole\Paris\ING4\S2\IAA\2024-ECE-Ing4-Fin-Sudoku-Gr02\Sudoku.SimulatedAnnealing\Resources\SimulatedAnnealing.py");
				string code = Resources.SimulatedAnnealing_py;
				scope.Exec(code);
				
				//Retrieve solved Sudoku variable
				var result = scope.Get("r");

				//Convert back to C# object
				var managedResult = result.As<int[][]>();
				//var convertesdResult = managedResult.Select(objList => objList.Select(o => (int)o).ToArray()).ToArray();
				return new Shared.SudokuGrid() { Cells = managedResult };
			}
			//}

		}

		protected override void InitializePythonComponents()
		{
			//declare your pip packages here
			InstallPipModule("numpy");
			InstallPipModule("simanneal");
			base.InitializePythonComponents();
		}

	}

