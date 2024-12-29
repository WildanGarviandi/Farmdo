using Godot;

public partial class PlayerCat : CharacterBody2D
{
	[Export]
	public float MoveSpeed { get; set; } = 100;

	[Export]
	public Vector2 StartingDirection { get; set; } = new Vector2(0, 1);
	
	private Logger logger;
	public AnimationTree _animationTree;
	public AnimationNodeStateMachinePlayback _animationStateMachine;

	public override void _Ready()
	{
		base._Ready();
		logger = new Logger(this);
		logger.trace("Here on ready");

		_animationTree = GetNode<AnimationTree>("AnimationTree");
		_animationTree.Set("parameters/Idle/blend_position", StartingDirection);
		_animationStateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
	}

	public override void _PhysicsProcess(double delta)
	{
		logger = new Logger(this);

		base._PhysicsProcess(delta);

		// Get input direction
		var input_direction = new Vector2(
			Input.GetActionStrength("right") - Input.GetActionStrength("left"),
			Input.GetActionStrength("down") - Input.GetActionStrength("up")
		).Normalized();

		UpdateAnimationParameters(input_direction);

		// Move and slide function uses velocity of character
		MoveAndSlide();
		PickNewState();
	}

	private void UpdateAnimationParameters(Vector2 MoveInput)
	{
		// Update velocity
		Velocity = MoveInput * MoveSpeed;

		if (MoveInput != Vector2.Zero)
		{
			_animationTree.Set("parameters/Walk/blend_position", MoveInput);
			_animationTree.Set("parameters/Idle/blend_position", MoveInput);
		}
	}

	private void PickNewState()
	{
		if (Velocity != Vector2.Zero)
		{
			_animationStateMachine.Travel("Walk");
		}
		else
		{
			_animationStateMachine.Travel("Idle");
		}
	}
}
