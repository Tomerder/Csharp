import sys
import string
import time
import threading

def ParseMulti(line, matrix):
    cleanLine=line.split(",")
    for col in range(0,5000):
        matrix[col] = cleanLine[col]


def SumMulti(matrix, row, buffer, tempSum):
    for col in range(0,5000):
        tempSum += int(matrix[col])
    buffer[row] = tempSum



print("Single Thread Results:")

#Read the File
start = time.time()
file = open("c:\Benchmark\Matrix.txt", "r")
end = time.time()
print("Read time is :" + str(end - start))
#Parse The Data
start = time.time()
row = 0
matrix = [[0 for x in range(5000)] for y in range(5000)]
for line in file: 
    col = 0
    cleanLine=line.split(",")
    for item in cleanLine:
        if (col < 5000):
            matrix[row][col] = item
            col += 1
    row+=1
end = time.time()
print("Parse time is :" + str(end - start))
parseTimeSingle = end - start
#Sum the Data
start = time.time()
sum = 0
for row in range(0,5000): 
    for col in range(0,5000):
        sum += int(matrix[row][col])
end = time.time()
print("Sum time is :" + str(end - start))
print("The sum is :" + str(sum))
sumTimeSingle = end - start


print("---------------------------------------------------------------")

print("Multi Thread Results:")

matrix2 = [[0 for x in range(5000)] for y in range(5000)]
buffer1 = [0 for x in range(5000)]
file2 = open("c:\Benchmark\Matrix.txt", "r")
start = time.time()
row = 0
threads = []*4
for line in file2:
    t = threading.Thread(target = ParseMulti, name = 'thread{}'.format(row), args =(line, matrix2[row]) )
    threads.append(t)
    t.start()
    row+=1
    
for i in threads:
    i.join()
end = time.time()
print("Multi-Parse time is :" + str(end - start))
parseTimeMulti = end - start

start = time.time()
tempSum = 0
threads2 = []*4
for row1 in range(0,5000):
    t2 = threading.Thread(target = SumMulti, name = 'thread{}'.format(row1), args =(matrix2[row1], row1, buffer1, tempSum))
    threads.append(t2)
    t2.start()
    
for i in threads2:
    i.join()
    
sumBuffer = 0
for item in buffer1:
    sumBuffer+=item
    
end = time.time()
print("Multi-Sum time is :" + str(end - start))
sumTimeMulti = end - start
print("The Multi sum is :" + str(sumBuffer))

print("---------------------------------------------------------------")
betterParse = (parseTimeSingle - parseTimeMulti)/(parseTimeSingle)*100
betterSum = (sumTimeSingle - sumTimeMulti)/(sumTimeSingle)*100 
print("Multi is better than Single in Parse: " + str(betterParse) + "%")
print("Multi is better than Single in Sum: " + str(betterSum) + "%")
