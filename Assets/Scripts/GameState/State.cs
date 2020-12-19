public abstract class State
{
	public readonly MovementStrategy movementStrategy;
	public State(MovementStrategy strategy)
	{
		movementStrategy = strategy;
	}
	public abstract State AdvanceState();
	public abstract State RetreatState();
	public abstract bool IsTextSpawningAllowed();

	public virtual void SetupPlayer(){}
}