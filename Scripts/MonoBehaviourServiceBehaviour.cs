using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DUCK.Utils
{
	public sealed class MonoBehaviourServiceBehaviour : MonoBehaviour
	{
		public Action OnUpdate { private get; set; }
		public Action OnLateUpdate { private get; set; }
		public Action OnFixedUpdate { private get; set; }
		public Action<bool> ApplicationPause { private get; set; }
		public Action ApplicationQuit { private get; set; }
		public Action<bool> ApplicationFocus { private get; set; }
		public Action OnLevelLoaded { private get; set; }

		private static MonoBehaviourServiceBehaviour instance;

		#region Scene Load Events

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				throw new Exception("There must only one service at a time");
			}

			SceneManager.sceneLoaded += HandleSceneLoaded;
		}

		private void HandleSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			if (OnLevelLoaded != null) OnLevelLoaded();
		}

		#endregion

		#region Update Events

		private void Update()
		{
			if (OnUpdate != null) OnUpdate();
		}

		private void LateUpdate()
		{
			if (OnLateUpdate != null) OnLateUpdate();
		}

		private void FixedUpdate()
		{
			if (OnFixedUpdate != null) OnFixedUpdate();
		}

		#endregion

		#region Application Events

		private void OnApplicationPause(bool paused)
		{
			if (ApplicationPause != null) ApplicationPause(paused);
		}

		private void OnApplicationQuit()
		{
			if (ApplicationQuit != null) ApplicationQuit();
		}

		private void OnApplicationFocus(bool focused)
		{
			if (ApplicationFocus != null) ApplicationFocus(focused);
		}

		#endregion

		#region Deconstruction

		private void OnDestroy()
		{
			SceneManager.sceneLoaded -= HandleSceneLoaded;
		}

		#endregion
	}
}
