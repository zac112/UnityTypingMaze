using UnityEngine;

public class GameState : State
{
	public GameState() : base(new LabyrinthMovement()) { }

	public override State AdvanceState()
	{
		Application.LoadLevel("MainMenu");
		return new MenuState();
	}

	public override State RetreatState()
	{
		Application.LoadLevel("MainMenu");
		return new MenuState();
	}

	public override bool IsTextSpawningAllowed()
	{
		return true;
	}

	public override void SetupPlayer ()
	{
		UISlider slider = GameObject.Find("Health Bar").GetComponent<UISlider>();
		Player.Instance().SetUISlider(slider);
		EventDelegate.Add(slider.onChange, Player.Instance().SetFromBar);
	}
}