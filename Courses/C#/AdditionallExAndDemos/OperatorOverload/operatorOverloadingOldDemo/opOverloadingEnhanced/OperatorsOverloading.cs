using System;

struct Date
{
	public Date (int day, int month, int year)
	{
//		if (isMonthOK (month))
			this.month = month;
		
//		if (isDayOK (day))
			this.day = day;
		
		this.year = year;
	}

	public Date (Date other)
	{
		day = other.day;
		month = other.month;
		year = other.year;
	}

	private int day;
	private int month;
	private int year;

	public int Day 
	{
		get
		{
			return day;
		}

		set
		{
			if (isDayOK (value))
				day = value;
		}
	}

	public int Month 
	{
		get
		{
			return month;
		}
		set
		{
			if (isMonthOK (value))
				month = value;
		}

	}

	public int Year 
	{
		get
		{
			return year;
		}
		set
		{
			year = value;
		}
	}	
	

	private bool isMonthOK (int month)
	{
		// This is a general, non-precise check, 
		// should be performed according to the month

		return (month > 0 && month < 13);
	}
	
	private bool isDayOK (int day)
	{
		// This is a general, non-precise check, 
		// should be performed according to the month

		return (day > 0 && day < 32);
	}
	
	public override bool Equals(object obj)	
	{
		if (obj == null || GetType() != obj.GetType()) 
			return false;

		Date other = (Date)obj;
		return day == other.day && month == other.month && year == other.year;
	}

	public override int GetHashCode()
	{
		return day + month * 100 + year * 10000;
	}

	public static bool operator == (Date d1, Date d2)
	{
		return d1.Equals(d2);
	}


	public static bool operator != (Date d1, Date d2)
	{
		return !(d1 == d2);	
	}

	public static bool operator > (Date d1, Date d2)
	{
		if (d1.year > d2.year)
			return true;
		else if (d1.year < d2.year)
			return false;

		// years are equal, lets try month
		if (d1.month > d2.month)
			return true;
		else if (d1.month < d2.month)
			return false;

		// month are equal, lets try day
		if (d1.day > d2.day)
			return true;
		else if (d1.day < d2.day)
			return false;

	// Dates are equal

		return false;
	}

	public static bool operator < (Date d1, Date d2)
	{
		return (d2 > d1);
	}

	public static bool operator >= (Date d1, Date d2)
	{
		return !(d1 < d2);
	}

	public static bool operator <= (Date d1, Date d2)
	{
		return !(d1 > d2);
	}

	public static Date operator++(Date d1)
	{
		d1.incrementDay ();	
		return d1;
	}	

	public static Date operator +(Date d1, int numDays)
	{
		Date retVal = new Date (d1);
		retVal.day += numDays;
		if (retVal.day > 31) 
		{
			retVal.day = retVal.day - 31;
			++ retVal.month;
			if (retVal.month > 12) 
			{
				retVal.month = 1;
				++ retVal.year;
			}
		}
		return retVal;
	}

	public override String ToString ()
	{
		return (String.Format("{0}.{1}.{2}", day.ToString(), month.ToString(), year.ToString()));
	}


	
	private void incrementDay()
	{
		++day;
		if (day == 32) 
		{
			day =1;
			if (month == 12) 
			{
				month =1;
				year++;
			}
			else
				month++;
			}
		}

}// end of class Date
	

class DateApp
{
	static void Main()
	{

		Date d1 = new Date (1, 1, 2000);	
		
		Console.WriteLine (d1);		

		Date d2 = d1;
		if (d2 == d1)
			Console.WriteLine ("Equal");
		
		Date d3 = new Date(1,1,2000);
		if (d3 != d1)
			Console.WriteLine ("Not Equal");
		
		Date d6 = d1++;
		d6 = ++d1;
		
		Date d4 = d1 + 5;
		d4 +=6;		

	}
}