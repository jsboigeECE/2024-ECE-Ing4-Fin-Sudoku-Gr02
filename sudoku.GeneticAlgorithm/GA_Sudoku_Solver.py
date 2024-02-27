import random as rndm
import time
import matplotlib.pyplot as plt
import numpy as np
import copy

def make_gene(initial):
    gene=copy.deepcopy(initial)
    number=[i for i in range(1,10)]
    rndm.shuffle(number)
    for nbr_gene in initial:
        if nbr_gene!=0:
            number.remove(nbr_gene)

    for i in range(0,9):
        if gene[i]==0:
            gene[i]=number.pop()
    return gene

def make_chromosome(initial):
    chromosome = []
    for i in range(9):
        chromosome.append(make_gene(initial[i]))
    return chromosome

def make_population(count, initial):
    population = []
    for _ in range(count):
        population.append(make_chromosome(initial))
    return population

def fitness_score(chromosome):
    score=0
        
    #Column Condition
    for i in range(0,9):
        score-=9-len(set([row[i] for row in chromosome]))
        
    #Square Condition
    for i in range(0,3):
        for j in range(0,3):
            Sub=[row[j*3:(j+1)*3] for row in chromosome[i*3:(i+1)*3]]
            score-=9-len(set(value for sublist in Sub for value in sublist))

    return score

def crossover(ch1, ch2):

    strategy=rndm.randint(0,2)  #I only take row and column
    strategy=0
    if strategy==0: #row
        new_child_1 = []
        new_child_2 = []
        for i in range(9):
            x = rndm.randint(0, 1)
            if x == 1:
                new_child_1.append(ch1[i])
                new_child_2.append(ch2[i])
            elif x == 0:
                new_child_2.append(ch1[i])
                new_child_1.append(ch2[i])

    elif strategy==1:   #column
        new_child_1 = []
        new_child_2 = []
        for i in range(9):
            x = rndm.randint(0, 1)
            if x == 1:
                new_child_1.append(ch1[:][i])
                new_child_2.append(ch2[:][i])
            elif x == 0:
                new_child_2.append(ch1[:][i])
                new_child_1.append(ch2[:][i])

    elif strategy==2:   #square
        new_child_1 = [[0]*9]*9
        new_child_2 = [[0]*9]*9
        for i in range(0,3):
            for j in range(0,3):
                x = rndm.randint(0, 1)
                if x == 1:
                    for i2 in range(i*3,(i+1)*3):
                        for j2 in range(j*3,(j+1)*3):
                            new_child_1[i2][j2]=ch1[i2][j2]
                            new_child_2[i2][j2]=ch2[i2][j2]
                elif x == 0:
                    for i2 in range(i*3,(i+1)*3):
                        for j2 in range(j*3,(j+1)*3):
                            new_child_1[i2][j2]=ch2[i2][j2]
                            new_child_2[i2][j2]=ch1[i2][j2]
                
    return new_child_1, new_child_2

def mutation(ch, pm, initial):
    for i in range(9):
        x = rndm.randint(0, 100)
        if x < pm * 100:
            ch[i] = make_gene(initial[i])
    return ch

def genetic_algorithm(initial,crossover_rate,mutation_gene_rate):  # Main genetic algorithm function

    #---------------------------------------------------------- Start Population
    M=[]
    population = make_population(POPULATION, initial)

    #---------------------------------------------------------- Fitness 
    fitness_list=[[fitness_score(chr),chr] for chr in population]   #fitness score list
    fitness_list.sort(key=lambda x:x[0])                            #sort fitness score list less to more
    m = fitness_list[-1][0]                                         #find best score
    M.append(m)                                                     #save best score
    print("best fitness score:",m)                                  #print best score
       
    m=-1
    for _ in range(GENERATION):
    #while m!=0 :
        
        #---------------------------------------------------------- Selection
        #Mating pool 
        pool=[chr[1] for chr in fitness_list]
        wall=int((1-crossover_rate)*len(pool))
        
        #Create Parent List
        elit_parent= pool[wall:]
        
        #Create no crossover list
        no_crossover_list=pool[len(pool)-wall:wall]

        #---------------------------------------------------------- Crossover
        #Create Child List
        nc_list=[]
        eltp_list=elit_parent.copy()
        rndm.shuffle(eltp_list)
        for i in range(0,int(len(elit_parent)/2)):
            p1=eltp_list.pop()
            p2=eltp_list.pop()
            nc1,nc2=crossover(p1,p2)
            nc_list.append(nc1)
            nc_list.append(nc2)

        #Create population after crossover
        pool=elit_parent+no_crossover_list

        #---------------------------------------------------------- Mutation
        mutation_list=[]
        for chr in pool:
            mutation_list.append(mutation(chr,mutation_gene_rate,initial))

        population=mutation_list+nc_list

        #---------------------------------------------------------- Fitness 
        fitness_list=[[fitness_score(chr),chr] for chr in population]   #fitness score list
        fitness_list.sort(key=lambda x:x[0])                            #sort fitness score list less to more
        m = fitness_list[-1][0]                                         #find best score
        M.append(m)                                                     #save best score
        print("best fitness score:",m)                                  #print best score
        #----------------------------------------------------------

        if m == 0:
            return population,M
        
    return population,M


t1=[[8, 0, 6, 0, 0, 0, 1, 0, 7],
    [0, 0, 0, 6, 0, 2, 0, 0, 0],
    [0, 5, 3, 0, 0, 4, 8, 0, 6], 
    [7, 0, 4, 8, 0, 0, 6, 3, 0], 
    [0, 0, 0, 0, 0, 0, 0, 9, 0], 
    [1, 0, 0, 5, 0, 0, 4, 0, 0], 
    [0, 0, 1, 2, 0, 0, 7, 0, 9], 
    [2, 0, 0, 0, 9, 6, 0, 0, 0], 
    [0, 7, 0, 0, 1, 0, 0, 8, 0]]

t2=[[4, 7, 0, 8, 0, 0, 0, 9, 0],
    [0, 0, 0, 0, 3, 0, 1, 0, 0],
    [0, 0, 6, 0, 0, 0, 0, 0, 0],
    [5, 6, 0, 0, 0, 1, 9, 0, 0],
    [0, 0, 8, 0, 0, 0, 0, 6, 0],
    [0, 0, 2, 5, 0, 0, 0, 0, 0],
    [9, 5, 0, 4, 0, 0, 0, 7, 0],
    [0, 0, 0, 0, 0, 8, 0, 0, 4],
    [2, 0, 0, 0, 0, 0, 0, 0, 0]]

POPULATION = 2000      # Population size
GENERATION = 30      # Number of generations
crossover_rate=0.4    #best 40% replace worst 40%
mutation_gene_rate=0.3


tic = time.time()
r,M= genetic_algorithm(t1,crossover_rate,mutation_gene_rate)
toc = time.time()

print("time taken:",toc-tic)
print("Amount of generation:",len(M)-1)
fit = [fitness_score(c) for c in r]

# Print the chromosome with the highest fitness
m=max(fit)
for c in r:
    if fitness_score(c) == m:
        print(np.array(c))
        break
plt.plot(M)
plt.show()
















