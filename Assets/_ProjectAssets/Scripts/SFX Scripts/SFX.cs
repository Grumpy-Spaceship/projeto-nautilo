﻿// Maded by Pedro M Marangon
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sounds
{
	[System.Serializable]
	public class SFX
	{

		[LabelText("SFX Type"), LabelWidth(100)]
		[OnValueChanged("SFXChange"), InlineButton("PlaySFX")]
		public SFXManager.SFXType sfxType = SFXManager.SFXType.UI;
		
		[LabelText("$sfxLabel"), LabelWidth(100), ValueDropdown("SFXType")]
		[OnValueChanged("SFXChange")]
#if UNITY_EDITOR
		[InlineButton("SelectSFX")]
#endif

		public SFXClip sfxToPlay;
		private string sfxLabel = "SFX";

		[SerializeField] private bool showSettings = false, editSettings = false;

		[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
		[ShowIf("showSettings"), EnableIf("editSettings"), SerializeField] private SFXClip _sfxBase;
		
		[Title("Audio Source")]
		[ShowIf("showSettings"), EnableIf("editSettings"), SerializeField] private bool waitToPlay = true;

		[ShowIf("showSettings"), EnableIf("editSettings"), SerializeField] private bool useDefault = true;

		[DisableIf("useDefault")]
		[ShowIf("showSettings"), EnableIf("editSettings"), SerializeField] private AudioSource audioSource;


		public AudioSource Source
		{
			get
			{
				if (useDefault || audioSource == null) return null;
				else return audioSource;
			}
		}

		private void SFXChange()
		{
			//keep the label up to date
			sfxLabel = sfxType.ToString() + " SFX";

			//keep the displayed "SFX clip" up to date;
			_sfxBase = sfxToPlay;
		}

#if UNITY_EDITOR
		private void SelectSFX() => UnityEditor.Selection.activeObject = sfxToPlay;
#endif

		//Get's a list of SFX from manager, used in the inspector
		private List<SFXClip> SFXType()
		{
			List<SFXClip> sfxList;

			switch (sfxType)
			{
				case SFXManager.SFXType.UI: default:
					sfxList = SFXManager.instance.uiSFX;
					break;
				case SFXManager.SFXType.Ambient:
					sfxList = SFXManager.instance.ambientSFX;
					break;
				case SFXManager.SFXType.Weapons:
					sfxList = SFXManager.instance.weaponSFX;
					break;
			}

			return sfxList;
		}

		public void PlaySFX()
		{
			if (useDefault || audioSource == null)
				SFXManager.PlaySFX(sfxToPlay, waitToPlay, null);
			else
				SFXManager.PlaySFX(sfxToPlay, waitToPlay, audioSource);
		}

	}
}