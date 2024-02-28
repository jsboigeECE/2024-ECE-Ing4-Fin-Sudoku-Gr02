import numpy as np
import random
import math
####################################################################################################

def initial_solution(sudoku):
    """
    Génère une solution initiale valide en respectant les chiffres déjà présents.
    Pour chaque ligne du Sudoku, cette fonction retire les nombres déjà présents,
    mélange les nombres restants, et remplit les cases vides.
    """
    sudoku = np.array(sudoku)  # Convertit sudoku en tableau numpy
    for i in range(9):  # i le nombre de lignes
        numbers = list(range(1, 10)) #crée une liste des chiffres de 1 à 9
        for j in range(9):    # On parcoure les colonnes
            if sudoku[i][j] != 0: # Si on rencontre un nombre différent de 0 (nombre fixe de base)
                numbers.remove(sudoku[i][j]) # Retire les nombres déjà présents de la liste numbers
        random.shuffle(numbers)   # Mélange les nombres restants pour un résultat aléatoire
        for j in range(9):
            if sudoku[i][j] == 0:
                sudoku[i][j] = numbers.pop()#Remplit les cases vides par un chiffre de la liste numbers réarrangée
    return sudoku


def energy(sudoku):
    """
    Calcule l'énergie (nombre de conflits) d'une solution.
    Compte le nombre de doublons dans chaque ligne, colonne et sous-grille 3x3.
    """
    conflicts = 0
    for i in range(9):
        #set sert à retirer les doublons (reste les éléments uniques)
        conflicts += 9 - len(set(sudoku[i]))  # Compte les conflits par ligne
        conflicts += 9 - len(set(sudoku[:,i]))  # Compte les conflits par colonne
    for i in range(0, 9, 3):
        for j in range(0, 9, 3):
            #transforme une sous-grille(3x3) en liste
            subgrid = sudoku[i:i+3, j:j+3]
            conflicts += 9 - len(set(subgrid.flatten()))  # Compte les conflits par sous-grille
    return conflicts


def neighbor(sudoku, instance):
    """
    Génère un voisin de la solution actuelle en échangeant deux chiffres dans une même ligne.
    Veille à ne pas modifier les chiffres initiaux de l'instance.
    """
    new_sudoku = np.copy(sudoku)
    row = random.randint(0, 8) # Sélection aléatoire du numéro de la ligne
    instance_row = np.array(instance[row])
    i, j = random.sample([k for k in range(9) if new_sudoku[row, k] not in instance_row], 2)
    new_sudoku[row, i], new_sudoku[row, j] = new_sudoku[row, j], new_sudoku[row, i] # Echange
    return new_sudoku

def solveSudoku(sudoku, max_iter=10000, initial_temp=10, cooling_rate=0.99):
    """
    Algorithme du recuit simulé pour trouver une solution au Sudoku.
    Accepte des solutions moins bonnes au début pour éviter les minima locaux,
    et devient plus strict au fur et à mesure que la température diminue.
    """
    current_solution = initial_solution(sudoku) # remplit cases vides (0)
    current_energy = energy(current_solution) # nb erreurs
    temp = initial_temp

    for _ in range(max_iter):
        # compare le nombre d'erreurs entre la grille avec sa voisine avec 2 chiffres échangés
        next_solution = neighbor(current_solution, sudoku)
        next_energy = energy(next_solution)
        delta_energy = next_energy - current_energy
        
        # accepte la nouvelle solution selon le critère de Metropolis
        if delta_energy < 0 or random.random() < math.exp(-delta_energy / temp):
            current_solution, current_energy = next_solution, next_energy
        
        temp *= cooling_rate  # Diminution de la température
        
        if current_energy == 0:  # arrête si une solution parfaite est trouvée
            break

    # Convertit la solution finale en un tuple de tuples
    return tuple(tuple(row) for row in current_solution.tolist())

# Instance de sudoku (pas besoin de conversion, gérée dans la fonction)
"""
instance = [[0,0,0,0,9,4,0,3,0],
            [0,0,0,5,1,0,0,0,7],
            [0,8,9,0,0,0,0,4,0],
            [0,0,0,0,0,0,2,0,8],
            [0,6,0,2,0,1,0,5,0],
            [1,0,2,0,0,0,0,0,0],
            [0,7,0,0,0,0,5,2,0],
            [9,0,0,0,6,5,0,0,0],
            [0,4,0,9,7,0,0,0,0]]
"""
solution = solveSudoku(instance)
r = solution  # r est maintenant un tuple de tuples