using UnityEngine;
using Utilities;
namespace Example
{
	public class CountdownTimerExample : MonoBehaviour
	{
		private CountdownTimer countdownTimer;

		private void Start()
		{
			countdownTimer = new CountdownTimer(1f); // Initialize with 10 seconds
			countdownTimer.OnTimerStart += () =>
			{
				Debug.Log("Countdown started!");
				Debug.Log("Press [Space] to reset the timer!");
			};
			countdownTimer.OnTimerStop += () => Debug.Log("Countdown stopped!");
			countdownTimer.Start();
		}

		private void Update()
		{
			if (countdownTimer.IsRunning)
			{
				countdownTimer.Tick(Time.deltaTime);
				Debug.Log($"Countdown Progress: {(int)(countdownTimer.Progress * 100)}%");

				if (countdownTimer.IsFinished)
				{
					Debug.Log("Countdown finished!");
				}
			}

			// Reset the timer with space key
			if (Input.GetKeyDown(KeyCode.Space))
			{
				countdownTimer.Reset();
				countdownTimer.Start();
			}
		}
	}
}