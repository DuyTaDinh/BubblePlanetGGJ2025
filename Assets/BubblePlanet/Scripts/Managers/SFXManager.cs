// using UnityEngine;
// using Utilities;
// namespace Managers
// {
// 	public class SFXManager : Singleton<SFXManager>
// 	{
// 		[Header("Audio Source")]
// 		[Space(10)]
// 		[SerializeField] AudioSource BGM;
// 		[SerializeField] AudioSource SFX;
// 		[SerializeField] AudioSource fightButtonSFX;
//
// 		[Header("Audio Clips")]
// 		[Space(10)]
// 		[SerializeField] AudioClip[] BGMClips;
// 		[SerializeField] AudioClip UIButton;
// 		[SerializeField] AudioClip UIUpgrade;
// 		[SerializeField] AudioClip UIPurchase;
// 		[SerializeField] AudioClip win;
// 		[SerializeField] AudioClip lose;
// 		[SerializeField] AudioClip pickUpGold;
// 		[SerializeField] AudioClip shot;
// 		[SerializeField] AudioClip jetpack;
// 		[SerializeField] AudioClip hit;
// 		[SerializeField] AudioClip heal;
// 		[SerializeField] AudioClip bossDead;
//
// 		public AudioSource BGMAudioSource { get => BGM; set => BGM = value; }
//
//
// 		protected override void Awake()
// 		{
// 			base.Awake();
// 			DontDestroyOnLoad(gameObject);
// 		}
// 		void Start()
// 		{
// 			// Vibration.Init();
// 		}
//
// 		public void ChangeBGM(int order)
// 		{
// 			SFX.Stop();
// 			if (BGM.clip != BGMClips[order])
// 			{
// 				BGM.clip = BGMClips[order];
// 				BGM.loop = true;
//
// 				if (SettingsManager.Instance.MusicState == 1)
// 				{
// 					BGM.Play();
// 					BGM.mute = false;
// 				}
// 				else
// 				{
// 					BGM.Stop();
// 					BGM.mute = true;
// 				}
// 			}
// 		}
//
//     #region =================== UI ===================
//
// 		public void Click()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(UIButton);
// 			}
// 		}
//
// 		public void Upgrade()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(UIUpgrade);
// 			}
// 		}
//
// 		public void Buy()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(UIPurchase);
// 			}
// 		}
//
// 		public void Win()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				BGM.Stop();
// 				SFX.PlayOneShot(win);
// 			}
// 		}
//
// 		public void Lose()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				BGM.Stop();
// 				SFX.PlayOneShot(lose);
// 			}
// 		}
//
//     #endregion
//
//     #region Gameplay
//
// 		public void PickUpGold()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(pickUpGold);
// 			}
// 		}
//
// 		public void Hit()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(hit);
// 			}
// 		}
//
// 		public void Heal()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(heal);
// 			}
// 		}
//
// 		public void Shot()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(shot);
// 			}
// 		}
//
// 		public void JetpackFire()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(jetpack);
// 			}
// 		}
//
// 		public void BossDead()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				SFX.PlayOneShot(bossDead);
// 			}
// 		}
//
//     #endregion
//
// 		public void Vibrate()
// 		{
// 			if (SettingsManager.Instance.VibraState == 1)
// 			{
// 				// Vibration.Vibrate(150);
// 			}
// 		}
//
// 		public void Fight()
// 		{
// 			if (SettingsManager.Instance.SoundState == 1)
// 			{
// 				fightButtonSFX.Play();
// 			}
// 		}
// 	}
// }