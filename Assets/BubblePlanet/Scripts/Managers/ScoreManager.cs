using TMPro;
using UnityEngine;
using Utilities;
namespace Managers
{
	public class ScoreManager : Singleton<ScoreManager>
	{
		[SerializeField] private TextMeshProUGUI scoreText;
		
		private int _score;
		
		protected override void Awake()
		{
			base.Awake();
			_score = 0;
			UpdateScoreText();
		}

		public void AddScore(int score)
		{
			_score += score;
			UpdateScoreText();
		}

		private void UpdateScoreText()
		{
			scoreText.text = _score.ToString();
		}
	}
}