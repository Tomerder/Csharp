import java.io.*;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
public class Benchmark2
{
  @SuppressWarnings("resource")
public static void main(String[] args) throws Exception
  {
	//Single:
	  System.out.println("Start the single activity");
	  
    // Read the file
	Long startTimeReadSingle = System.currentTimeMillis();
	BufferedReader br =
	new BufferedReader(new FileReader("C:\\Benchmark\\Matrix.txt"));
	Long endTimeReadSingle = System.currentTimeMillis(); 
	System.out.println("Time Read Single: " + (endTimeReadSingle - startTimeReadSingle));
	
	
	//Parse the Data Single
	Long startTimeParseSingle = System.currentTimeMillis();
	long[][] matrix = new long[5000][5000];    
    int row = 0, col = 0;
    String line;
    while (((line = br.readLine()) != null) && (row<5000))
    {
    	col = 0;
    	String[] array = line.split(",");
    	for (String item : array){
    		if (col < 5000)
    		{
    			matrix[row][col] = Long.parseLong(item, 10);
    			col++;
    		}
    	}
    	row++;
    }
    Long endTimeParseSingle = System.currentTimeMillis();
    System.out.println("Time Parse Single: " + (endTimeParseSingle - startTimeParseSingle));
    
    
	//Sum the Data Single
	Long startTimeSumSingle = System.currentTimeMillis();
	long sum = 0;
    for (int rowIndex = 0; rowIndex < 5000; rowIndex++)
    {
        for (int colIndex = 0; colIndex < 5000; colIndex++)
        {
            sum += ((matrix[rowIndex][colIndex]));
        }
    }   	
    Long endTimeSumSingle = System.currentTimeMillis();
    System.out.println("Time Sum Single: " + (endTimeSumSingle - startTimeSumSingle));
    System.out.println("The Sum is: "+ sum);
    System.out.println("-----------------------------------------");
    
    
    //System.gc();
    

    
		//Multithread:
		System.out.println("Start the Multithread activity");
		  
		// Read the file
		Long startTimeReadMulti = System.currentTimeMillis();
		BufferedReader br2 =
		new BufferedReader(new FileReader("C:\\Benchmark\\Matrix.txt"));
		Long endTimeReadMulti = System.currentTimeMillis(); 
		System.out.println("Time Read Multithread: " + (endTimeReadMulti - startTimeReadMulti));
		
		//Parse the Data Multithread		
		ExecutorService executor = Executors.newFixedThreadPool(4);
		Long startTimeParseMulti = System.currentTimeMillis();
	    long[][] matrix1 = new long[5000][5000];    
	    int row1 = 0;
	    String line1;
	    while (((line1 = br2.readLine()) != null) && (row1<5000))
	    {    		    	
	    	Runnable ParseMulti = new ParseMulti(line1, matrix1[row1]);
            executor.execute(ParseMulti);	    	
	    	row1++;
	    }
	    executor.shutdown();
	    while(!executor.isTerminated()){}
	    Long endTimeParseMulti = System.currentTimeMillis();
	    System.out.println("Time Parse Multi: " + (endTimeParseMulti - startTimeParseMulti));
	    
		//Sum the Data Multi
	    
	    ExecutorService executor2 = Executors.newFixedThreadPool(4);
	    Long tempSum = (long) 0;	    
		Long startTimeSumMulti = System.currentTimeMillis();   
	    Map<Integer, Long> bufferNew = new HashMap<Integer, Long>();
	    for (int rowIndex = 0; rowIndex < 5000; rowIndex++)
	    {		
	    	Runnable SumMulti = new SumMulti(rowIndex, bufferNew, matrix1[rowIndex], tempSum);
	    	executor2.execute(SumMulti);
	    	
	    }   
	    executor2.shutdown();
	    while(!executor2.isTerminated()){}	
	    Long sumMulti = (long) 0;
	    sumMulti = bufferNew.values().stream().mapToLong(Long::longValue).sum();	 
	    Long endTimeSumMulti = System.currentTimeMillis();
	    System.out.println("Time Sum Multi: " + (endTimeSumMulti - startTimeSumMulti));
	    System.out.println("The Sum is: "+ sumMulti);	    
	    System.out.println("-----------------------------------------");	    
	    float tempParseSingle = endTimeParseSingle - startTimeParseSingle;
	    float tempParseMulti = endTimeParseMulti - startTimeParseMulti;
	    System.out.println("Multi is better than single in Parse: " + (tempParseSingle - tempParseMulti)/(tempParseSingle)*100 + "%");	    
	    float tempSumSingle = endTimeSumSingle - startTimeSumSingle;
	    float tempSumMulti = endTimeSumMulti - startTimeSumMulti;
	    System.out.println("Multi is better than single in Sum: " + (tempSumSingle - tempSumMulti)/(tempSumSingle)*100 + "%");
		
  }
}
