#if TOOLS
using Godot;
using System;

[Tool]
public partial class BuffEditor : EditorPlugin
{
	private Control _control = GD.Load<PackedScene>("res://scenes/buffEditor.tscn").Instantiate<Control>();

	private Button _load;
	
	public override void _EnterTree()
	{
		// Initialization of the plugin goes here.
		AddControlToDock(DockSlot.LeftBr, _control);
		// _load = _control.GetChild<VBoxContainer>(1).GetChild<Button>(1);

	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveControlFromDocks(_control);
        // Erase the control from the memory.
        _control.Free();
	}
}
#endif
