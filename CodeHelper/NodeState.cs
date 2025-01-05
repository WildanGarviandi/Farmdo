using Godot;

public partial class NodeState : Node
{
    [Signal]
    public delegate void TransitionEventHandler();

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public void NextTransition()
    {

    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}
