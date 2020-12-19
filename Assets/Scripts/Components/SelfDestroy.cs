using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
		public float timeToWait = 3f;

		IEnumerator Start ()
		{
				yield return new WaitForSeconds (timeToWait);
				Debug.Log ("destroying");
				Destroy (gameObject);
		}
}