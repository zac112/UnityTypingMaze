using UnityEngine;

public class SineWaveMovement : TextMovement
{
	[SerializeField]
	private GameObject trail;
	[SerializeField]
	private float frequency = 1f;
	[SerializeField]
	private float wavelength = 1f;

	protected override float GetY()
	{
		return Mathf.Sin(2 * Mathf.PI * Time.time * frequency) * wavelength;
	}

	protected override void Move()
	{
		base.Move();
		// sin fun
//		Instantiate(trail, cachedTransform.position, cachedTransform.rotation);
	}
}