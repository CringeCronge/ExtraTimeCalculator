using Godot;
using System;

public partial class ExtraTime : Control
{
	[Signal]
	public delegate void EndTimeUpdateEventHandler(string newText);

	public void UpdateEndTimeEventHandler(string newText)
	{
		EmitSignal(SignalName.EndTimeUpdate, newText);
	}
}
