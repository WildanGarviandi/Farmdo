using System;
using Godot;

public partial class Cow : CharacterBody2D
{
	[Export]
	public float MoveSpeed { get; set; } = 20;

	[Export]
	public float IdleTime { get; set; } = 5;

	[Export]
	public float WalkTime { get; set; } = 2;

	public enum COW_STATE 
	{
		IDLE,
		WALK
	}

	private Logger logger;
	public AnimationTree _animationTree;
	public AnimationNodeStateMachinePlayback _animationStateMachine;
	public Sprite2D _spriteCow;
	public Timer _timer;

	private Vector2 MoveDirection = Vector2.Zero;
	private COW_STATE CurrentState = COW_STATE.IDLE;

	public override void _Ready()
	{
		base._Ready();
		logger = new Logger(this);
		logger.trace("Here on ready cow");

		_animationTree = GetNode<AnimationTree>("AnimationTree");
		_animationStateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
		_spriteCow = GetNode<Sprite2D>("Sprite2D");
		_timer = GetNode<Timer>("Timer");

		PickNewState();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (CurrentState == COW_STATE.WALK) {
			Velocity = MoveDirection * MoveSpeed;
			MoveAndSlide();	
		}
	}

	public void OnTimerTimeout()
	{
		PickNewState();
	}

	private void SelectNewDirection()
	{
		var values = new[] { -1, 1 };
		MoveDirection = new Vector2(
			values[new Random().Next(values.Length)],
			values[new Random().Next(values.Length)]
		);

		logger.trace($"New direction {MoveDirection.X}");
		if (MoveDirection.X < 0)
		{
			_spriteCow.FlipH = true;
		} 
		else
		{
			_spriteCow.FlipH = false;
		}
	}

	private void PickNewState()
	{
		if (CurrentState == COW_STATE.IDLE)
		{
			_animationStateMachine.Travel("Walk");
			CurrentState = COW_STATE.WALK;
			SelectNewDirection();
			_timer.Start(WalkTime);
		}
		else
		{
			_animationStateMachine.Travel("Idle");
			CurrentState = COW_STATE.IDLE;
			_timer.Start(IdleTime);
		}
	}

}
