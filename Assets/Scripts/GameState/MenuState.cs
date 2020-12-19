using UnityEngine;

public class MenuState : State
{

	public MenuState() : base(new NullMovement()) { }

	public override State AdvanceState()
	{
		Application.LoadLevel(2);
		return new GameState();
	}

	public override State RetreatState()
	{
		Application.LoadLevel(0);
		return new IntroState();
	}

	public override bool IsTextSpawningAllowed()
	{
		return false;
	}
}