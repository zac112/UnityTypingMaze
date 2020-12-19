using UnityEngine;

public abstract class TextMovement : MonoBehaviour
{
	public float minSpeed = 20f;
	public float maxSpeed = 100f;
	public float minInitialOrientation;
	public float maxInitialOrientation;
	private Vector3 bottomLeft;
	private Vector3 topRight;
	[SerializeField]
	protected float speed;
	protected Vector3 velocity;
	protected Vector3 direction;
	protected Transform mTransform;
	protected Transform cachedTransform { get { if (!mTransform) mTransform = transform; return mTransform; } }
	protected virtual void Move()
	{
		velocity.y = GetY();
		cachedTransform.localPosition += (velocity + direction) * Time.deltaTime * speed;
		ReactToEdges();
	}
	protected virtual void ReactToEdges()
	{
		if (cachedTransform.localPosition.x <= bottomLeft.x ||
			cachedTransform.localPosition.x >= topRight.x)
			direction += new Vector3(-2 * direction.x, 0, 0);

		if (cachedTransform.localPosition.y <= bottomLeft.y ||
			cachedTransform.localPosition.y >= topRight.y)
			direction += new Vector3(0, -2 * direction.y, 0);

	}
	protected virtual void Start()
	{
		speed = Random.Range(minSpeed, maxSpeed);
		var background = cachedTransform.parent.GetComponent<UIWidget>();
		bottomLeft = background.localCorners[0];
		topRight = background.localCorners[2];
		InitDirection();
	}
	protected virtual void Update()
	{
		Move();
	}
	protected virtual void InitDirection()
	{
		float randZ = Random.Range(minInitialOrientation, maxInitialOrientation);
		direction = Quaternion.Euler(0, 0, randZ) * cachedTransform.right;
	}
	protected abstract float GetY();
}