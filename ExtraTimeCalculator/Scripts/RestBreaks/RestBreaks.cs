using Godot;
using System;

public partial class RestBreaks : GridContainer
{
	private Label TotalTime;
	private Button StartStop;
	private Label RestBreakList;
	private Button ResetAll;
	private Button ResetRecent;
	private ulong _startTime, _endTime;
	[Export]
	public int _totalDuration = 0;
	private int _currentBreak, _breakCount = 1;
	[Signal]
	private delegate void RestBreakUpdateEventHandler(int totalDuration);
	
	public override void _Ready()
	{
		TotalTime = GetNode<Label>("TotalTime");

		StartStop = GetNode<Button>("StartStop");
		StartStop.Toggled += OnStartStopToggled;

		RestBreakList = GetNode<Label>("ScrollContainer/RestBreaks/PseudoList");

		ResetAll = GetNode<Button>("CenterContainer/ResetOptions/ResetAll");
		ResetRecent = GetNode<Button>("CenterContainer/ResetOptions/ResetRecent");

		ResetAll.Pressed += ResetAllBreaks;
		ResetRecent.Pressed += ResetRecentBreak;
	}

	private void OnStartStopToggled(bool toggledOn)
	{
		if (toggledOn)
		{
			StartStop.Text = "Stop Rest Break";
			_startTime = Time.GetTicksMsec();
		}
		else
		{
			StartStop.Text = "Start Rest Break";
			_endTime = Time.GetTicksMsec();
			CalculateDurations(_startTime, _endTime);
			AddRestBreak();
			EmitSignal(SignalName.RestBreakUpdate, _totalDuration);
		}
	}

	private void CalculateDurations(ulong startTime, ulong endTime)
	{
		_currentBreak = (int)(1+((endTime-startTime)/60_000L));
		_totalDuration += _currentBreak;
		TotalTime.Text = $"Total minutes: {_totalDuration}";		
	}

	private void AddRestBreak()
	{
		RestBreakList.Text += $"Break {_breakCount}: {_currentBreak} minutes\n";
		_breakCount++;
	}

	private void ResetAllBreaks()
	{
		EmitSignal(SignalName.RestBreakUpdate, 0);
		
		_totalDuration = 0;
		_currentBreak = 0;
		_startTime = 0;
		_endTime = 0;
		_breakCount = 1;
		StartStop.ButtonPressed = false;
		TotalTime.Text = "Total Time: 0 minutes";
		RestBreakList.Text = "";
	}

	private void ResetRecentBreak()
	{
		var breaks = RestBreakList.Text.Split("\n");

		if (breaks.Length < 2)
		{
			ResetAllBreaks();
		}
		else
		{
			string newList = "";
			for(int i = 0; i < breaks.Length-2; i++)
			{
				newList += breaks[i] + "\n";
			}
			RestBreakList.Text = newList;
			_breakCount--;

			var breakDuration = breaks[^2].Split(" ");
			_totalDuration -= int.Parse(breakDuration[^2]);
			TotalTime.Text = $"Total Time: {_totalDuration} minutes";
			EmitSignal(SignalName.RestBreakUpdate, _totalDuration);
		}
	}

}
