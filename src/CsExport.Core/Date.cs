using System;

namespace CsExport.Core
{
	public class Date
	{
		private int _year;
		private int _month;
		private int _day;

		public int Year { get { return _year; } }
		public int Month { get { return _month; } }
		public int Day { get { return _day; } }

		public Date(DateTime dateTime)
		{
			var date = dateTime.Date;

			_day = date.Day;
			_month = date.Month;
			_year = date.Year;
		}

		public Date(int year, int month, int day) : this(new DateTime(year, month, day))
		{	
		}  

		public override string ToString()
		{
			return $"{Year}-{Month}-{Day}";
		}

		public DateTime GetDateTime()
		{
			return new DateTime(Year, Month, Day, 0, 0, 0);
		}

		public override bool Equals(object obj)
		{
			var source = this;
			var target = obj as Date;
			if (target == null)
				return false;

			return source.Day.Equals(target.Day)
			       && source.Month.Equals(target.Month)
			       && source.Year.Equals(target.Year);
		}

		public override int GetHashCode()
		{
			return Day*Month*37 + Year*97;
		}
	}
}