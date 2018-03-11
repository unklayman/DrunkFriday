using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager {

	private Dictionary <string, UnityEvent<EventBase>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (eventManager == null)
            {
				eventManager = new EventManager();

                if (eventManager == null)
                {
                    Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init (); 
                }
            }

            return eventManager;
        }
    }

    void Init ()
    {
        if (eventDictionary == null)
        {
			eventDictionary = new Dictionary<string, UnityEvent<EventBase>>();
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
        if (eventManager == null) return;
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