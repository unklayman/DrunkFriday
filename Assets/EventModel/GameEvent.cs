using System;
using UnityEngine.Events;

/**
 *  This class needs us because UnityEvent<T> is abstract dunno why
 */
public class GameEvent : UnityEvent<EventBase>
{
}
