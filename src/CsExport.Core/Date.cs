using System;

namespace CsExport.Core
{
	public struct Date
	{
		public short Year { get; private set; }
		public byte Month { get; private set; }
		public byte Day { get; private set; }

		public Date(DateTime dateTime)
		{
			var date = dateTime.Date;

			Day = (byte) date.Day;
			Month = (byte) date.Month;
			Year = (short) date.Year;
		}

		public Date(short year, byte month, byte day) : this(new DateTime(year, month, day))
		{	
		}

		public override string ToString()
		{
			return $"{Year}-{Month}-{Day}";
		}
	}
}