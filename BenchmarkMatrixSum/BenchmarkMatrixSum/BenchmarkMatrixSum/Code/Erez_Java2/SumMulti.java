import java.util.Map;

class SumMulti implements Runnable {  
	int row1;
	Map<Integer, Long> buffer1;
	long[] matrix1;	 
	Long tempSum1;	
	SumMulti(int row, Map<Integer, Long> buffer, long[] matrix, Long tempSum) {row1 = row;buffer1 = buffer; matrix1 = matrix; tempSum1 = tempSum;}
  public void run(){	  
      for (int colIndex = 0; colIndex < 5000; colIndex++)
      {
    	  tempSum1 += matrix1[colIndex];
      }
      buffer1.put(row1, tempSum1);   
  }

}  