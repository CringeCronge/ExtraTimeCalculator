using Godot;
using System;
using System.Data;
using System.Linq;

public partial class Inputs : Node
{
	
	private LineEdit _DefualtDuration;
	private LineEdit _ExtraTime;
	private LineEdit _StartTime;

	private string _Output = "00:00";
	private int[] _InputTime = new int[2];

	[Signal]
	public delegate void UpdateEndTimeEventHandler(string input);
	
	public override void _Ready()
	{
		_DefualtDuration = GetChild<LineEdit>(0);
		_ExtraTime = GetChild<LineEdit>(1);
		_StartTime = GetChild<LineEdit>(2);

		_DefualtDuration.TextChanged += OnDefualtDurationTextChanged;
		_ExtraTime.TextChanged += OnExtraTimeTextChanged;
		_StartTime.TextSubmitted += OnStartTimeTextSubmitted;
	}
	
	private void OnExtraTimeTextChanged(string newText)
	{
		EmitSignal(SignalName.UpdateEndTime, newText);
	}

	private void OnDefualtDurationTextChanged(string newText)
	{
		EmitSignal(SignalName.UpdateEndTime, newText);
	}

	private void OnStartTimeTextSubmitted(string newText)
	{
		EmitSignal(SignalName.UpdateEndTime, newText);
		
	}
	
}
