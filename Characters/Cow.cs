using System;
using Godot;

public partial class Cow : CharacterBody2D
{
	[Export]
	public float MoveSpeed { get; set; } = 30;

	public enum COW_STATE 
	{
		IDLE,
		WALK
	}

	private Logger logger;
	public AnimationTree _animationTree;
	public AnimationNodeStateMachinePlayback _animationStateMachine;

	private Vector2 MoveDirection = Vector2.Zero;
	private COW_STATE CurrentState = COW_STATE.IDLE;

	public override void _Ready()
	{
		base._Ready();
		logger = new Logger(this);
		logger.trace("Here on ready cow");

		_animationTree = GetNode<AnimationTree>("AnimationTree");
		_animationStateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");

		SelectNewDirection();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		Velocity = MoveDirection * MoveSpeed;

		MoveAndSlide();
	}

	private void SelectNewDirection()
	{
		MoveDirection = new Vector2(
			new Random().Next(-1, 1),
			new Random().Next(-1, 1)
		);

		if (MoveDirection.X == 0)
		{
			
		}
	}

	private void PickNewState()
	{
		if (CurrentState == COW_STATE.IDLE)
		{
			_animationStateMachine.Travel("Walk");
			CurrentState = COW_STATE.WALK;
		}
		else
		{
			_animationStateMachine.Travel("Idle");
			CurrentState = COW_STATE.IDLE;
		}
	}

}
