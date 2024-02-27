# Résolution de Sudokupar Optimisation par essaims particulaires (PSO) 

## Définition

	**L'optimisation par essaim particulaire** (PSO) est une méthode de résolution de problèmes qui s'inspire du comportement social des essaims d'insectes et des bancs de poissons dans la nature. 
	Au cœur de cette méthode, un ensemble de solutions potentielles, représentées par des particules, explore l'espace du problème à la recherche de la solution optimale.
	Dans cette approche itérative, chaque particule de l'essaim représente une solution possible et possède deux caractéristiques principales : sa **position** et sa **vitesse**.
	Les particules se déplacent dans l'espace du problème en ajustant leur vitesse en fonction de leur propre expérience (la meilleure solution qu'elles ont trouvée jusqu'à présent) et de l'expérience collective de tout l'essaim (la meilleure solution globale).
	Le processus est itératif, chaque particule mettant à jour sa trajectoire en fonction des meilleures positions connues.
	Ce mécanisme encourage l'échange d'informations entre les particules, favorisant ainsi l'amélioration globale de l'ensemble de l'essaim vers la meilleure solution possible.

## Objectif de notre projet

	Notre **but** est de se servir de cette méthode pour développer des **solveurs de Sudoku** en utilisant le **langage C#**
	
### Prérequis 

	Environnements : **Visual Studio Community** / **Rider Jet Brains**

	Un dépôt de code est fourni avec les projets de base facilitant la réalisation du projet.

	Dans le projet Sudoku.Shared de la solution, on trouve les classes suivantes :

		**- Classe SudokuGrid** : elle représente l'état d'un Sudoku à résoudre ou en cours de résolution.
			Elle possède une **méthode ToString()** permettant d'afficher l'état d'un Sudoku sous forme de
			chaîne de caractères, et des **méthodes Parse()** qui prennent en paramètre un fichier de Sudokus
			ou les lignes de son contenu et renvoie une liste d’objets de la classe nouvellement créée
	    **- Interface ISolverSudoku** : elle définit les méthodes à implémenter par les solvers de Sudoku.
	    **- Classe PythonSolverBase** : Il s’agit d’une classe de base héritable pour implémenter un sol-
			ver en Python utilisant le bridge Python.Net

	Dans le **projet Sudoku.Benchmark** de la solution, on trouve la **classe Program**: elle permet de
	tester les solvers de Sudoku de façon individuelle ou dans le cadre d'un benchmark comparatif ;
	Elle utilise la classe **Stopwatch** pour mesu rer les temps d'exécution des solvers.

## PSOsolver version 1

	




### Performances

	 **- Easy -**  Time to solution : **33,41 ms**
	 **- Medium -** Time to solution : **57320,34 ms**
	 **- Difficult -** Time to solution : **75722,249 ms**

	 Nous constatons que cette version de solverPSO est **très efficace** pour les grilles de Sudoku de difficulté **Easy**
	 En revanche, elle ne l'est pas pour les difficultés supérieures. Pour obtenir de meilleurs résultats, l'idéal est de s'appuyer sur la librairie suivante:
	 https://github.com/yasserglez/metaheuristics

## Metaheuristics

	Afin d'avoir de meilleures performances pour les difficultés supérieures, nous pouvons utiliser la librairie **metaheuristics** grâce au dépôt précédant.

### Definition 

	Une **métaheuristique** représente une catégorie d'**algorithmes d'optimisation** conçus pour résoudre des problèmes difficiles issus de divers domaines tels que la recherche opérationnelle, l'ingénierie ou l'intelligence artificielle. 
	Ces problèmes souvent complexes ne peuvent être efficacement résolus par des méthodes conventionnelles.
	Les métaheuristiques sont généralement des **algorithmes itératifs** et **stochastiques**. Ils progressent vers une solution optimale globale, souvent désignée comme l'extremum global d'une fonction, en échantillonnant une fonction objectif.
	Fonctionnant comme des **algorithmes de recherche**, les métaheuristiques s'adaptent au problème en apprenant ses caractéristiques afin de converger vers une solution optimale, généralement une approximation, qui se rapproche de l'optimum global. Cette approche est similaire à celle des algorithmes d'approximation.
	

### Définition TSP (Travelling Salesman Problem)

	Le **TSP (Travelling Salesman Problem)** est un problème de **combinatoire** qui constitue un défi d'optimisation.
	Il consiste à trouver le parcours le plus court qui relie un ensemble donné de villes en les visitant chacune exactement une fois.
	Ce problème est emblématique en informatique, suscitant un grand intérêt de recherche et servant souvent d'introduction à l'algorithmique et à la théorie de la complexité.
	Il trouve de nombreuses applications pratiques, notamment dans la planification, la logistique et des domaines plus éloignés tels que la génétique, où les villes représentent des gènes et la distance entre elles reflète leur similarité.


## PSOsolver version 2




### Performances globales 

	**Coloration Graph Solver** easy 1 :  65,1349 ms 
	**CSPchoco** easy 1 :  53,8512 ms 
	**Simulatedannealing** easy 1 : 27,2353 ms
	**Backtrackingdotnet** easy 1 : 2,4842 ms

## Difficultés générales rencontrées

	- Difficultés pour Pull pour avoir les ajouts des autres solvers des autres groupes quand on est dans la partie Choose a solver.
	- Conflits lors des fusions avec les modifications précédentes du professeur qu'on n'avait pas récupéré.
    - Choix des fichiers utils pour notre cas car il y a plusieurs fichiers PSO différents (Best, First). On les a regardé mais on a eu du mal à retranscrire le problème TSP en problème Sudoku.


## Journal de bord

	Résumé de ce qu'on a réalisé **en temps réel**

### Premier Pull-Request (PR) : Ajout projet PSO

	• **Fork** du repository principal sur Github et attribué les droits d’édition sur le fork à l’équipe en charge du projet dans l’onglet settings de notre dépôt
	• Clonage local du fork sur chacune des machines des membres du groupe
	• Chargement de la solution existante *.sln 
	• Définition du projet **Sudoku.Benchmark** comme **projet de démarrage**
	• Exécution du projet **Benchmark** pour vérifier que l’application est fonctionnelle
	• Création d'un nouveau projet de type bibliothèque de classe de préférence 
	• Dans le nouveau projet référenciation du projet noyau **Sudoku.Shared** contenant la définition d’un Sudoku et de l’interface de solver
	

### Deuxième PR : PSO Première Tentative 

#### Liste des classes 

	**1- PSOSolver** 
	**2- Particle** (avec ses constructeurs)

#### Liste des méthodes 

	**1- UpdatePosition**, qui nous permet de mettre à jour les positions des particules
    **2- SudokuGrid Solve**, qui nous permet de résoudre la grille de Sudoku
	  - En dernière partie du code de la classe **Particle** en commentaire, on a cherché à implémenter une méthode **UpdateVelocity** pour calculer la mise à jour des vitesses des particules

#### Difficultés 

	- Comprendre la **théorie** de la méthode PSO
	- Quand nous lançons la première version de notre code, il y a des **erreurs** sur **Sudoku.Benchmark**

### Troisième PR : PSOsolver de base fonctionnel 

#### Liste des nouvelles classes du dépôt SudokuCombinatorialEvolutionSolver 

	**3- MatrixHelper**
	**4- Organism**
	**5- OrganismType**
	**6- Sudoku**

	Notre premier solver résout les Sudoku de niveau - Easy - mais il prend plus de temps (environ 1min) pour résoudre les Sudoku de difficultés supérieures. 
	Maintenant, on se penche sur la librairie Heuristiclab afin d'utiliser metaheuristics. On regarde également l'adaptation TSP / PSO pour optimiser notre Solver.
	
### Quatrième PR (Partiel) : Avancement solver PSO avec metaheuristics 

	Il nous faut concevoir la traduction du Sudoku en problème compatible grâce au PSO discrétisé (DiscretePSO), qui est implémenté dans le dépôt **metaheuristics**. 
	On s'est inspiré de ce qui a été fait pour le **TSP** (cf définition plus haut)
	L'adaptation TSP/PSO a été effectuée dans ce dépôt et présente de bons résultats. 

    Pour comprendre cela, nous avons cloné et testé le code du dépot en question. Comme il y a beaucoup de tests effectués, nous avons fait le ménage en commentant ce qui n'est pas utile, c'est à dire que nous avons commenté tout dans le fichier **Main** à l'exception par exemple de la ligne 

	```
	new PSO2OptBest4TSP(),

	```
    






