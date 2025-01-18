using System;
using UnityEngine;
namespace Gameplay
{
	[Serializable]
	public class Sound
	{
		public string name;

		public AudioClip clip;
		[Range(0f, 1f)] public float volume = 0.05f;
		[Range(0.1f, 3f)] public float pitch = 1;
		public bool loop;

		[HideInInspector] public AudioSource source;
	}
}