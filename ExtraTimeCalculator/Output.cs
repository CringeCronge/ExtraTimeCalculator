using Godot;
using System;

public partial class Output : Label
{
	private Node _Inputs;
	public override void _Ready()
	{
	   Node _Inputs = GetNode("%Inputs");
	}

	public void UpdateEndTimeEventHandler(string input)
	{
		this.Text = input;
	}
}
