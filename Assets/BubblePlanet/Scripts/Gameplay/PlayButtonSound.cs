using Managers;
using UnityEngine;
namespace Gameplay
{
	public class PlayButtonSound : MonoBehaviour
	{
		// Triggered by the unity event attached to the button
		public void PlaySound()
		{
			AudioManager.Instance.PlaySound(SoundName.ButtonSelect);
		}
	}
}