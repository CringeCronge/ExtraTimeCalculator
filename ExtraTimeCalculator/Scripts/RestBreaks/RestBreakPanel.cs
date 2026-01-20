using Godot;
using System;

public partial class RestBreakPanel : Control
{
	[Signal]
	private delegate void UpdateRestBreaksEventHandler(int additionalDuration);

	private void RestBreakUpdate(int newTime)
	{
		EmitSignal(SignalName.UpdateRestBreaks, newTime);
	}
}
