﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarTime
{

	private static int day = 0, hour = 0, minute = 0;

	public static int Day
	{
		get
		{
			return day;
		}
		set
		{
			day = value;
		}
	}

	public static int Hour
	{
		get
		{
			return hour;
		}
		set
		{
			hour = value;
		}
	}

	public static int Minute
	{
		get
		{
			return minute;
		}
		set
		{
			minute = value;
		}
	}


}
