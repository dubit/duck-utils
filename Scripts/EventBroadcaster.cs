using System;
using UnityEngine;

namespace DUCK.Utils
{
	public static class EventBroadcaster
	{
		public static void BroadcastEvent<T>(this Component component, Action<T> execute, bool broadcastToChildren = true)
		{
			component.gameObject.BroadcastEvent(execute, broadcastToChildren);
		}

		public static void BroadcastEvent<T>(this GameObject gameObject, Action<T> execute, bool broadcastToChildren = true)
		{
			if (broadcastToChildren)
			{
				var children = gameObject.GetComponentsInChildren<T>();
				foreach (var child in children)
				{
					execute(child);
				}
			}
			else
			{
				var components = gameObject.GetComponents<T>();
				foreach (var component in components)
				{
					execute(component);
				}
			}
		}
	}
}