using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {

	public float scrollSpeed = 0.5F;

	// Update is called once per frame
	void Update () {
	
		float offset = Time.time * scrollSpeed;
		renderer.material.mainTextureOffset = new Vector2(0, offset);
		/*
		float yOffSet = Mathf.PingPong(Time.time,1f);

		renderer.material.mainTextureOffset = new Vector2(0f,yOffSet);*/
	}
}
