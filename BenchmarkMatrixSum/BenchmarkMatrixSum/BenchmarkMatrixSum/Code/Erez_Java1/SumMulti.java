import java.util.concurrent.atomic.AtomicLong;

class SumMulti implements Runnable {  
	int row1;
	AtomicLong totalSum1;
	long[] matrix1;	 
	long tempSum1;	
	SumMulti(int row, AtomicLong totalSum, long[] matrix, long tempSum) {row1 = row; totalSum1 = totalSum; matrix1 = matrix; tempSum1 = tempSum;}
  public void run(){	  
      for (int colIndex = 0; colIndex < 5000; colIndex++)
      {
    	  tempSum1 += matrix1[colIndex];
      }

      totalSum1.getAndAdd(tempSum1);
  }

}  