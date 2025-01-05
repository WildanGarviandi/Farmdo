using System;
using Godot;
using Godot.Collections;

public partial class NodeStateMachine: Node
{
    [Export]
    public NodeState InitialNodeState { get; set; }
    private Dictionary node_states;
    private NodeState current_node_state;
    private string current_node_state_name;
    private string parent_node_state_name;

    public override void _Ready()
    {
        base._Ready();
        foreach (var child in GetChildren())
        {
            if (child.GetType() == typeof(NodeState)) {
                node_states[child.Name] = child;
            }
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public void TransitionTo()
    {

    }
}
