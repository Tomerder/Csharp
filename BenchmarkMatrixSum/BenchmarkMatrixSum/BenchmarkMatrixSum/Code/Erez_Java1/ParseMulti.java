class ParseMulti implements Runnable {  
	String line1;
	long[] matrix1;
	ParseMulti(String line, long[] matrix) {line1 = line; matrix1 = matrix; }
  public void run(){
	  String[] line = line1.split(",");
	  	for (int index = 0; index < 5000; index++)
	  	{	  			
	  		matrix1[index] = Long.parseLong(line[index], 10);	  						
		}
  }

 } 
