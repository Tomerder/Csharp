#!/ Ruby test/usr/bin/env ruby

# Read matrix from file
start = Time.now
arr = Array[5000]
i = 0
sum = 0
File.readlines("Matrix.txt").map do |line|
  arr[i] = line.split(",").map(&:to_i)
  i = i+1
end
finish = Time.now
calc = finish - start
print "It took ", calc, " sec to read matrix\n"

# Calculate sum of all variables in matrix
start = Time.now
for i in 0...5000
  arr[i].each {|num| sum+=num}
end
finish = Time.now
calc = finish - start
print "It tooks ", calc," sec to calc sum \nThe calculate sum is ", sum
