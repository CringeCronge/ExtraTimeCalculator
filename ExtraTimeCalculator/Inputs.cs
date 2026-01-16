using Godot;
using System;

public partial class Inputs : Node
{
	
	private LineEdit _DefualtDuration;
	private LineEdit _ExtraTime;
	private LineEdit _StartTime;
	
	public override void _Ready()
	{
		_DefualtDuration = GetChild<LineEdit>(0);
		_ExtraTime = GetChild<LineEdit>(1);
		_StartTime = GetChild<LineEdit>(2);

		_DefualtDuration.TextChanged += OnDefualtDurationTextChanged;
		_ExtraTime.TextChanged += OnExtraTimeTextChanged;
		_StartTime.TextSubmitted += OnStartTimeTextSubmitted;
	}

    private void OnStartTimeTextSubmitted(string newText)
    {
        GD.Print(newText);
    }


    private void OnExtraTimeTextChanged(string newText)
    {
        GD.Print(newText);
    }

    private void OnDefualtDurationTextChanged(string newText)
	{
		GD.Print(newText);
	}
	
}
