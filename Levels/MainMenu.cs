using Godot;

public partial class MainMenu : Control
{
	private Logger logger;

	public override void _Ready()
	{
		base._Ready();
		logger = new Logger(this);
	}

	public void StartMenuPressed()
	{
		logger.trace("Start pressed");
		GetTree().ChangeSceneToFile("res://Levels/game_level.tscn");
	}

	public void OptionsMenuPressed()
	{
		logger.trace("Options pressed");
	}

	public void ExitMenuPressed()
	{
		logger.trace("Exit!");
		GetTree().Quit();
	}
}
