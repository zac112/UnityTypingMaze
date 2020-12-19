using UnityEngine;

public static class GameStateManager
{
	private static State currentState = new IntroState();

	static GameStateManager()
	{
		EventManager.Subscribe<OnPlayerDiedEvent>(OnPlayerDiedHandler);
	}

	private static void OnPlayerDiedHandler(OnPlayerDiedEvent e)
	{
		AdvanceState();
	}
	public static void AdvanceState()
	{
		currentState = currentState.AdvanceState();
	}

	public static Vector2 ProcessKey(string kc)
	{
		return currentState.movementStrategy.GetDirection(kc);
	}

	public static bool ValidDestination(Vector2 destination)
	{
		return currentState.movementStrategy.ValidDestination(destination);
	}

	public static void SetupPlayer(){
		currentState.SetupPlayer();
	}
	
}
