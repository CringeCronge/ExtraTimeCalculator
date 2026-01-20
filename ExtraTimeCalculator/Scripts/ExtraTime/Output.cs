using Godot;
using System;

public partial class Output : Label
{
	public void UpdateEndTimeEventHandler(string input)
	{
		this.Text = input;
	}
}
