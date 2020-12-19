using UnityEngine;

public class IntroState : State
{

	public IntroState() : base(new IntroMovement()) { }

	public override State AdvanceState()
	{
		Application.LoadLevel("MainMenu");
		return new MenuState();
	}

	public override State RetreatState()
	{
		Application.Quit();
		return this;
	}

	public override bool IsTextSpawningAllowed()
	{
		return false;
	}
}