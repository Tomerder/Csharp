package main

import (
    "fmt"
    "strconv"
	"os"
	"encoding/csv"
	"log"
	"runtime"
	"sync"
	"time"
	"bufio"
	"strings"
)
var g_sum uint64
var mutex = &sync.Mutex{}
var c = make(chan int, runtime.NumCPU()) 
var g_array [5000][5000]int

func sumPartialMatrix(records[][]string, startLine int, endLine int){
	for i := startLine; i < endLine; i++ {
		for _, cell := range records[i] {
			if cell != "" {
				x, err := strconv.Atoi(cell)
				mutex.Lock()
				g_sum += uint64(x)
				mutex.Unlock()
				if err != nil {
					log.Fatal(err)
				}
			}	
		}
	}
	c <- 1
}

// readLines reads a whole file into memory
// and returns a slice of its lines.
func readLines(path string) ([]string, error) {
  file, err := os.Open(path)
  if err != nil {
    return nil, err
  }
  defer file.Close()

  var lines []string
  scanner := bufio.NewScanner(file)
  for scanner.Scan() {
    lines = append(lines, scanner.Text())
  }
  return lines, scanner.Err()
}


func readAll(records[][]string) uint64 {
	var sum uint64
	
	for _, h := range records {
		for _, cell := range h {
			if cell != "" {
				x, err := strconv.Atoi(cell)
				sum += uint64(x)
				if err != nil {
					log.Fatal(err)
				}
			}
		}
	}
	return sum
}


func parseToMatrix (lines []string , fromLine int, numOfLines int) {
	for i := fromLine; numOfLines > 0; i++ {
		strNums := strings.Split(lines[i],",")
		for j, strNum := range strNums {
			if strNum != "" {
				n, err := strconv.Atoi(strNum)
				if err != nil {
					log.Fatal(err)
				}
				g_array[i][j] = n
			}
		}
		numOfLines--
	}
	c <- 1
}

func main() {
	var numCPU = runtime.NumCPU()
	fmt.Println("CPUs: ",numCPU)
	var start time.Time
	var elapsed time.Duration
	
	// Measure Read file
	start = time.Now()
    
	// Read File
	file, err := os.Open("Matrix.txt")
    r := csv.NewReader(file)
	
	elapsed = time.Since(start)
    fmt.Println("Read file took ", elapsed)
	
	// Measure Parse to matrix (Single thread)
	start = time.Now()
	
	// Parse to matrix (Single thread)
	records, err := r.ReadAll()
	if err != nil {
		log.Fatal(err)
	}
	
	elapsed = time.Since(start)
    fmt.Println("Parse to matrix (Single thread) took ", elapsed)

	
	lines, err := readLines("Matrix.txt")
	if err != nil {
		log.Fatalf("readLines: %s", err)
	}
	
	// Measure Parse to matrix (Multiple threads)
	start = time.Now()
	
	// Parse to matrix (Multiple threads)
	for i := 0; i < numCPU; i++ {
		go parseToMatrix(lines,i*len(lines)/numCPU,len(lines)/numCPU)
	}
	// Drain the channel.
    for i := 0; i < numCPU; i++ {
        <-c    // wait for one task to complete
    }
	elapsed = time.Since(start)
    fmt.Println("Parse to matrix (Multiple threads) took ", elapsed)
	
	// Calculate sum (Multiple threads)
	start = time.Now()
    for i := 0; i < numCPU; i++ {
        go sumPartialMatrix(records, i*len(records)/numCPU, (i+1)*len(records)/numCPU)
    }
    // Drain the channel.
    for i := 0; i < numCPU; i++ {
        <-c    // wait for one task to complete
    }
    // All done.
	
	elapsed = time.Since(start)
	fmt.Println("Sum: ",g_sum)
    fmt.Println("Calculate sum (Multiple threads) took ", elapsed)

	
	// Calculate sum (Single thread)
	start = time.Now()
	g_sum = readAll(records)
	elapsed = time.Since(start)
	fmt.Println("Sum: ",g_sum)
	fmt.Println("Calculate sum (Single thread) took: ",elapsed)

}