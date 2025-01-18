// using UnityEngine;
// using Utilities;
// namespace Managers
// {
// 	public class SettingsManager : Singleton<SettingsManager>
// 	{
// 		[SerializeField] GameObject soundOnBtn;
// 		[SerializeField] GameObject soundOffBtn;
// 		[SerializeField] GameObject musicOnBtn;
// 		[SerializeField] GameObject musicOffBtn;
// 		[SerializeField] GameObject vibraOnBtn;
// 		[SerializeField] GameObject vibraOffBtn;
// 		[SerializeField] int soundState, musicState, vibraState;
//
// 		public int SoundState { get => soundState; set => soundState = value; }
// 		public int MusicState { get => musicState; set => musicState = value; }
// 		public int VibraState { get => vibraState; set => vibraState = value; }
//
// 		void Start()
// 		{
// 			soundState = PlayerPrefs.GetInt("Sound", 1);
// 			if (soundState == 0)
// 			{
// 				if (soundOffBtn != null) soundOffBtn.SetActive(true);
// 				if (soundOnBtn != null) soundOnBtn.SetActive(false);
// 			}
// 			else
// 			{
// 				if (soundOffBtn != null) soundOffBtn.SetActive(false);
// 				if (soundOnBtn != null) soundOnBtn.SetActive(true);
// 			}
//
// 			musicState = PlayerPrefs.GetInt("Music", 1);
// 			if (musicState == 0)
// 			{
// 				if (musicOffBtn != null) musicOffBtn.SetActive(true);
// 				if (musicOnBtn != null) musicOnBtn.SetActive(false);
// 				SFXManager.Instance.BGMAudioSource.mute = true;
// 			}
// 			else
// 			{
// 				if (musicOffBtn != null) musicOffBtn.SetActive(false);
// 				if (musicOnBtn != null) musicOnBtn.SetActive(true);
// 				SFXManager.Instance.BGMAudioSource.mute = false;
// 			}
//
// 			vibraState = PlayerPrefs.GetInt("Vibra", 1);
// 			if (vibraState == 0)
// 			{
// 				if (vibraOffBtn != null) vibraOffBtn.SetActive(true);
// 				if (vibraOnBtn != null) vibraOnBtn.SetActive(false);
// 			}
// 			else
// 			{
// 				if (vibraOffBtn != null) vibraOffBtn.SetActive(false);
// 				if (vibraOnBtn != null) vibraOnBtn.SetActive(true);
// 			}
//
// 			SFXManager.Instance.BGMAudioSource.Play();
// 		}
//
// 		public void ToggleSound()
// 		{
// 			soundState = 1 - soundState;
// 			if (soundState == 0)
// 			{
// 				soundOffBtn.SetActive(true);
// 				soundOnBtn.SetActive(false);
// 			}
// 			else
// 			{
// 				soundOffBtn.SetActive(false);
// 				soundOnBtn.SetActive(true);
// 			}
// 			PlayerPrefs.SetInt("Sound", soundState);
// 			SFXManager.Instance.Click();
// 		}
//
// 		public void ToggleMusic()
// 		{
// 			musicState = 1 - musicState;
// 			if (musicState == 0)
// 			{
// 				musicOffBtn.SetActive(true);
// 				musicOnBtn.SetActive(false);
// 				SFXManager.Instance.BGMAudioSource.mute = true;
// 			}
// 			else
// 			{
// 				musicOffBtn.SetActive(false);
// 				musicOnBtn.SetActive(true);
// 				SFXManager.Instance.BGMAudioSource.mute = false;
// 			}
// 			PlayerPrefs.SetInt("Music", musicState);
// 			SFXManager.Instance.Click();
// 		}
//
// 		public void ToggleVibra()
// 		{
// 			vibraState = 1 - vibraState;
// 			if (vibraState == 0)
// 			{
// 				vibraOffBtn.SetActive(true);
// 				vibraOnBtn.SetActive(false);
// 			}
// 			else
// 			{
// 				vibraOffBtn.SetActive(false);
// 				vibraOnBtn.SetActive(true);
// 			}
// 			PlayerPrefs.SetInt("Vibra", vibraState);
// 			SFXManager.Instance.Click();
// 		}
// 	}
//
// }