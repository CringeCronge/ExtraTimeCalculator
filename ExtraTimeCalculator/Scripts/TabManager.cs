using Godot;
using System;
using System.Collections.Generic;

public partial class TabManager : TabContainer
{
	private Button AddTab;
	private Button DeleteTab;

	public override void _Ready()
	{
		AddTab = GetNode<Button>("Set Up/GridContainer/AddTab");
		DeleteTab = GetNode<Button>("Set Up/GridContainer/DeleteTab");

		AddTab.Pressed += AddNewTab;
		DeleteTab.Pressed += DeleteLastTab;
	}

	private void AddNewTab()
	{
		PackedScene newTab = ResourceLoader.Load<PackedScene>("res://Scenes/DefualtTab.tscn");
		AddChild(newTab.Instantiate(), true, InternalMode.Disabled);
	}

	private void DeleteLastTab()
	{
		if(GetChildCount()>1)
		{
			var tabToDelete = GetChild(GetChildCount()-1);
			if (tabToDelete.IsClass("GridContainer"))
			{
				RemoveChild(tabToDelete);
			}
			else
			{
				RemoveChild(GetChild(GetChildCount()-2));
			}
		}
	}
}
