from timeit import default_timer as timer
import re
import threading
import multiprocessing

def calc_sum_mthread(matrix, threadnum):
    index = 0
    for array in matrix:
        #numarr = map(int, array)
        if (index % numOfCpus) == threadnum:
            SumArr[threadnum] += sum(array)
        index = index + 1

def parse_matrix_lines(lines):
    matrix = []
    for line in lines:
        #matrix.append(re.findall(r'\b\d+\b', line))
        matrix.append(map(int,re.findall(r'\b\d+\b', line)))
    return matrix

# Read Marix File
start = timer()
matrixFile = open('Matrix.txt','r')
readlines = matrixFile.readlines()
end = timer()
TimeReadFile = end - start
print('Read text file took ' + str(TimeReadFile) + ' seconds')

# Parse Matrix Single threaded

start = timer()
matrix = parse_matrix_lines(readlines)
end = timer()
ParseData = end - start
print('Parse matrix in single thread took ' + str(ParseData) + ' seconds')

# Calculate Matrix Sum Single threaded
start = timer()
summery = 0
for array in matrix:
    #numarr = map(int, array)
    summery += sum(array)
end = timer()
SumCalcTime = end - start
print('Calculate sum of matrix in single thread took ' + str(SumCalcTime) + ' seconds')

numOfCpus = multiprocessing.cpu_count()
matrixes = []
parsethreads = []
# Parse Matrix Multi threaded
start = timer()

end = timer()
ParseMthreadTime = end - start
print('Parse matrix in multi thread took ' + str(ParseMthreadTime) + ' seconds')

# Calculate Matrix Sum Multi threaded
calcthreads = []
SumArr = [0,0,0,0]
start = timer()
for index in range(0,numOfCpus):
    calcthreads.append(threading.Thread(target = calc_sum_mthread, args = (matrix, index)))
for thread in calcthreads:
    thread.start()
for thread in calcthreads:
    thread.join()
Sum = sum(SumArr)
end = timer()
SumMthreadCalcTime = end - start
print('Calculate sum of matrix in multi thread took ' + str(SumMthreadCalcTime) + ' seconds')