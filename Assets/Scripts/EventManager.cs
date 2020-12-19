using System.Collections.Generic;

public abstract class GameEvent
{
}

public static class EventManager
{
	public delegate void EventDelegate<T>(T e) where T : GameEvent;
	private delegate void EventDelegate(GameEvent e);

	private static Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
	private static Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

	public static void Subscribe<T>(EventDelegate<T> del) where T : GameEvent
	{
		// Early-out if we've already registered this delegate
		if (delegateLookup.ContainsKey(del))
			return;

		// Create a new non-generic delegate which calls our generic one.
		// This is the delegate we actually invoke.
		EventDelegate internalDelegate = (e) => del((T)e);
		delegateLookup[del] = internalDelegate;

		EventDelegate tempDel;
		if (delegates.TryGetValue(typeof(T), out tempDel)) {
			delegates[typeof(T)] = tempDel + internalDelegate;
		}
		else {
			delegates[typeof(T)] = internalDelegate;
		}
	}

	public static void Unsubscribe<T>(EventDelegate<T> del) where T : GameEvent
	{
		EventDelegate internalDelegate;
		if (delegateLookup.TryGetValue(del, out internalDelegate)) {
			EventDelegate tempDel;
			if (delegates.TryGetValue(typeof(T), out tempDel)) {
				tempDel -= internalDelegate;
				if (tempDel == null) {
					delegates.Remove(typeof(T));
				}
				else {
					delegates[typeof(T)] = tempDel;
				}
			}
			delegateLookup.Remove(del);
		}
	}

	public static void Raise(GameEvent e)
	{
		EventDelegate del;
		if (delegates.TryGetValue(e.GetType(), out del)) {
			del.Invoke(e);
		}
	}
}