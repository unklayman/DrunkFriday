using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager {

	private Dictionary <string, UnityEvent<EventBase>> eventDictionary = new Dictionary<string, UnityEvent<EventBase>>();

	protected static EventManager instance;

	public static EventManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new EventManager();
			}

			return instance;
		}
	}

	public static void StartListening (string eventName, UnityAction<EventBase> listener)
    {
		UnityEvent<EventBase> thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.AddListener (listener);
        } 
        else
        {
			thisEvent = new GameEvent();
            thisEvent.AddListener (listener);
            instance.eventDictionary.Add (eventName, thisEvent);
        }
    }

	public static void StopListening (string eventName, UnityAction<EventBase> listener)
    {
		if (instance == null) return;
		UnityEvent<EventBase> thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }

	public static void TriggerEvent (string eventName, EventBase evnt)
    {
		UnityEvent<EventBase> thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke (evnt);
        }
    }
}