//Maded by Pedro M Marangon
using NaughtyAttributes;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Player
{

	[DisallowMultipleComponent]
	public class KillPlayerOnContact : MonoBehaviour
	{
		#region COLLIDER CONFIG

#if UNITY_EDITOR
#pragma warning disable 0414
		private bool AddedCol = false;
#pragma warning restore 0414

		[HideIf("AddedCol"), Button("Add Capsule Collider")]
		private void AddCapsule()
		{
			gameObject.AddComponent<CapsuleTrigger2D>();
			AddedCol = true;
		}
		[HideIf("AddedCol"), Button("Add Circle Collider")]
		private void AddCircle()
		{
			gameObject.AddComponent<CircleTrigger2D>();
			AddedCol = true;
		}
		[HideIf("AddedCol"), Button("Add Box Collider")]
		private void AddBox()
		{
			gameObject.AddComponent<BoxTrigger2D>();
			AddedCol = true;
		}
		private void OnValidate()
		{
			if (TryGetComponent<Collider2D>(out var c))
			{
				c.isTrigger = true;
				AddedCol = true;
			}
		}
#endif

		#endregion

		public void OnTriggerEnter2D(Collider2D other)
		{
			Process(other);
		}

		private void Process(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				WhatToDo(other);
			}
		}

		protected virtual void WhatToDo(Collider2D other)
		{
			if (other.TryGetComponent<PlayerScript>(out var p))
			{
				Destroy(p.gameObject);
				FindObjectOfType<SceneTransitions>()?.ReloadScene(1.5f);
			}
		}
	}
}