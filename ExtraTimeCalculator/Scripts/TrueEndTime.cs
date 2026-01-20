using Godot;
using System;

public partial class TrueEndTime : Label
{
	private string endTime = "00:00";
	
	private void NewEndTime(string newText)
	{
		endTime = newText;
		this.Text = newText;
	}

	private void additionalDuration(int additionalMinutes)
	{
		string[] time = endTime.Split(":");
		int hours = int.Parse(time[0]), minutes = int.Parse(time[1]);

		if (minutes + additionalMinutes > 59)
		{
			if (hours + 1 > 23)
			{
				hours = 0;
				minutes = additionalMinutes+minutes-60;
			}
			else
			{
				hours++;
				minutes = additionalMinutes+minutes-60;
			}
		}
		else if (minutes + additionalMinutes < 0)
		{
			if (hours < 0)
			{
				hours = 23;
				minutes = minutes-additionalMinutes+60;
			}
			else
			{
				hours--;
				minutes = minutes-additionalMinutes+60;
			}
		}
		else
		{
			minutes += additionalMinutes;
		}

		if (minutes < 10)
		{
			this.Text = $"{hours}:0{minutes}";
		}
		else
		{
			this.Text = $"{hours}:{minutes}";
		}
	}
}
