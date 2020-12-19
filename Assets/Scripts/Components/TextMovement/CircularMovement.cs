using UnityEngine;

public class CircularMovement : TextMovement
{
	[SerializeField]
	private float radius;
	protected override float GetY()
	{
		return Mathf.Sqrt(radius - velocity.x * velocity.x);
	}
//	protected override void Move()
//	{
//		velocity.y = GetY();
//		cachedTransform.localPosition += (velocity + direction) * Time.deltaTime * speed;
////		float rotZ = Vector3.Angle(cachedTransform.right, velocity);
////		cachedTransform.Rotate(0, 0, rotZ);
//		ReactToEdges();
//	}
}