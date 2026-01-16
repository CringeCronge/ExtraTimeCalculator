using Godot;
using System;
using System.Data;
using System.Linq;

public partial class Inputs : Node
{
	//Nodes:
	private LineEdit _DefualtDuration;
	private LineEdit _ExtraTime;
	private LineEdit _StartTime;

	[Signal]
	public delegate void UpdateEndTimeEventHandler(string input);

	//Properties:
	private int[] startTime = new int[2];
	private int defualtDuration = 0;
	private int extraTime = 0;
	
	public override void _Ready()
	{
		_DefualtDuration = GetChild<LineEdit>(0);
		_ExtraTime = GetChild<LineEdit>(1);
		_StartTime = GetChild<LineEdit>(2);

		_DefualtDuration.TextChanged += OnDefualtDurationTextChanged;
		_ExtraTime.TextChanged += OnExtraTimeTextChanged;
		_StartTime.TextSubmitted += OnStartTimeTextSubmitted;
	}
	
	private void OnDefualtDurationTextChanged(string newText)
	{
		defualtDuration = int.Parse(newText);
		Update();
	}
	private void OnExtraTimeTextChanged(string newText)
	{
		extraTime = int.Parse(newText);
		Update();
	}

	private void OnStartTimeTextSubmitted(string newText)
	{		
		if (newText.Contains(":"))
		{
			string[] input = newText.Split(":");
			int hours = int.Parse(input[0]), minutes = int.Parse(input[1]);

			if (minutes > -1 && minutes < 60 && hours > -1 && hours < 24)
			{
				startTime[0] = hours;
				startTime[1] = minutes;
			}
		}

		Update();
	}

	void Update()
	{
		CalculateEndTime(startTime, extraTime, defualtDuration);
	}

	void CalculateEndTime(int[] startTime, int extraTime, int duration)
	{
		int totalDuration = (int)((float)duration*((float)duration/100));
		int hours = startTime[0], minutes = startTime[1];

		if(totalDuration > 59)
		{
			hours += totalDuration/60;
			totalDuration -= hours*60;
		}
		if(minutes + totalDuration > 59)
		{
			hours += 1;
			minutes += totalDuration - 60;
		}
		if (hours > 23)
		{
			hours = hours%24; //this allows for int.MaxValue hours!
		}

		string endTime = minutes < 10 ? $"{hours}:0{minutes}" : $"{hours}:{minutes}";
		EmitSignal(SignalName.UpdateEndTime, endTime);
	}
	
}
