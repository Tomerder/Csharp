using System;

struct Celsius
{
    public Celsius(float temp)
    {
        this.temp = temp;
    }
    

	public static implicit operator Fahrenheit(Celsius c)
	{
		return(((c.temp * 9) / 5) + 32);				
	}

	public static implicit operator Celsius(float temp)
	{
		Celsius c;
		c = new Celsius(temp);
		return(c);
	}

	public static implicit operator float(Celsius c)
	{
		return c.temp;		
	}

	public static explicit operator short(Celsius c)
	{
		return (short)c.temp;		
	}


	public override String ToString()
	{
		return (String.Format("{0} Cl", (int)temp));
	}

    private float temp;
}

struct Fahrenheit
{
    public Fahrenheit(float temp)
    {
        this.temp = temp;
    }

    
	public static implicit operator Celsius(Fahrenheit f)
	{
		return((((f.temp - 32) / 9) * 5));
	}

	public static implicit operator Fahrenheit(float temp)
	{
		Fahrenheit f;
		f = new Fahrenheit(temp);
		return(f);
	}

	public static implicit operator float(Fahrenheit f)
	{
		return f.temp;
	}

	
	public override String ToString()
	{
		return (String.Format("{0} Fr", (int)temp));
	}

    private float temp;
}

class TempratureApp
{
    public static void Main()
    {
        Celsius cl = new Celsius (0);
		displayTemprature(cl);	

		Fahrenheit fr = new Fahrenheit(0);
		displayTemprature(fr);	

		cl += 20.2F;
		displayTemprature(cl);	
        
        fr = cl;
		
		fr += 2;				
		displayTemprature(fr);			

		bool cold = isCold(cl);
		cold = isCold(fr);
		
		short s = (short)cl;
	}

	public static void displayTemprature(Celsius c)
	{
		Console.WriteLine ("The current temprature is:\n(in Celsius) {0},  (in Fahrenheit) {1}",c, (Fahrenheit)c); 		
		Console.WriteLine();
	}

	public static void displayTemprature(Fahrenheit f)
	{
		Console.WriteLine ("The current temprature is:\n(in Fahrenheit) {0},  (in Celsius) {1}", f, (Celsius)f); 		
		Console.WriteLine();
	}

	public static bool isCold (Celsius temp)
	{
		if (temp < 18)
			return true;
		return false;
	}
}
