using UnityEngine;

public class MainMenu : MonoBehaviour
{
	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2-25,200,50),"See how far the rabbit hole goes.")) {
			GameStateManager.AdvanceState();
		}

		if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2+50,200,50),"Wake up in your own bed."))
			Application.Quit();
	}
}