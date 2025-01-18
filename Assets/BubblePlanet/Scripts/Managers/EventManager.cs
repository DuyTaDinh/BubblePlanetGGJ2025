using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
namespace Managers
{
	public static class EventManager
	{
		private static Dictionary<string, Action<int>> eventDictionary = new Dictionary<string, Action<int>>();
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize()
		{
			eventDictionary = new Dictionary<string, Action<int>>();
		}
		
		public static void StartListening(string eventName, Action<int> listener)
		{

			Action<int> thisEvent;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent += listener;
				eventDictionary[eventName] = thisEvent;
			}
			else
			{
				thisEvent += listener;
				eventDictionary.Add(eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, Action<int> listener)
		{
			Action<int> thisEvent;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent -= listener;
				eventDictionary[eventName] = thisEvent;
			}
		}

		public static void TriggerEvent(string eventName, int i)
		{
			Action<int> thisEvent = null;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent?.Invoke(i);
			}
		}
	}
}