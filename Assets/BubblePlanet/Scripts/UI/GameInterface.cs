using System;
using Managers;
using TMPro;
using UnityEngine;
namespace UI
{
	public class GameInterface : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _scoreText;
		
		void Update()
		{
			_scoreText.text = $"{DataManager.Instance.GetScore().ToString()} (km)";
		}
	}
}