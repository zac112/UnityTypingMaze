using System;
using UnityEngine;

public class Player : MonoBehaviour
{

	private static Player player;
	public static Player Instance()
	{
		if (player == null) {
			throw new UnityException("No Player found in scene. Something has gone wrong!");
		}
		return player;
	}

	private Transform _transform; //cache for performance, I couldn't help but prematurely optimize =(
	public UISlider healthSlider;
	private Vector2 location;

	public float movementDuration = .5f;
	public bool forceWASD = false;
	public float Score { set; get; }
	public float Timer { private set; get; }

	void Awake()
	{
		Timer = 0;
		_transform = transform;
		player = this;
		location = new Vector2(_transform.position.x, _transform.position.y);

		GameStateManager.SetupPlayer();
	}

	void OnEnable()
	{
		EventManager.Subscribe<OnSuccessfullyTypedTextEvent>(OnMove);
	}

	private void OnMove(OnSuccessfullyTypedTextEvent e)
	{
		Move(e.typedText.PlayerMoveDir);
	}

	void Update()
	{
		Timer += Time.deltaTime;
		if (!string.IsNullOrEmpty(Input.inputString)) {
			if (forceWASD) {
				IntroMovement g = new IntroMovement();
				Move(g.GetDirection(Input.inputString));
			}
			else
				Move(GameStateManager.ProcessKey(Input.inputString));
		}
	}

	public void Move(Vector2 direction)
	{
		if (direction == Vector2.zero) return;

		Vector3 destination = _transform.position + new Vector3(direction.x, direction.y, 0);
		if (GameStateManager.ValidDestination(destination)) {
			location = destination;
			TweenPosition.Begin(gameObject, movementDuration, destination);
		}

	}

	public Vector2 GetLocation() { return location; }

	void OnDisable()
	{
		EventManager.Unsubscribe<OnSuccessfullyTypedTextEvent>(OnMove);
	}

	/* <<< HEALTH STUFF >>> */
	#region
	private int mCurrentHealth = 100;
	public int CurrentHealth
	{
		set
		{
			mCurrentHealth = Mathf.Clamp(value, 0, 100);
			healthSlider.value = mCurrentHealth / 100f;
			if (mCurrentHealth <= 0)
				Die();
		}
		get
		{
			return mCurrentHealth;
		}
	}
	public void ReceiveHealth(int amount)
	{
		CurrentHealth += amount;
	}
	public void ReceiveDamage(int amount)
	{
		CurrentHealth -= amount;
	}

	// called by the Slider bar when we slide it (HealthBar) - why slider bar? for testing
	public void SetFromBar()
	{
		if (UISlider.current) // current is a static reference to the current slider being modified
			mCurrentHealth = (int)(UISlider.current.value * 100);
	}

	public void SetUISlider(UISlider slider){
		healthSlider = slider;
	}
	#endregion

	public void Die()
	{
		CalculateScore();
		EventManager.Raise(new OnPlayerDiedEvent(this));
	}

	public void CalculateScore()
	{
		try
		{
			float timeTookToFinish = Timer;
			Score += CurrentHealth * 10;
			Score += 1 / timeTookToFinish;
			var typingman = GameObject.Find("Managers").GetComponent<TypingManager>();
			Score += 1 / typingman.Typos;
			Score += (typingman.TotalTypedLetters - typingman.Typos) * 10;
			print("FINALL SCORE: " + Score);
		}
		catch { }
	}
}

public class OnPlayerDiedEvent : GameEvent
{
	public readonly Player player;
	public OnPlayerDiedEvent(Player player)
	{
		this.player = player;
	}
}