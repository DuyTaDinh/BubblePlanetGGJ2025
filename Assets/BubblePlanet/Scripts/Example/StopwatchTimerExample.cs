using UnityEngine;
using Utilities;
namespace Example
{
	public class StopwatchTimerExample: MonoBehaviour
	{
		private StopwatchTimer stopwatchTimer;
		
		private void Start()
		{
			stopwatchTimer = new StopwatchTimer();
			stopwatchTimer.OnTimerStart += () =>
			{
				Debug.Log("Stopwatch started!");
				Debug.Log("Press [T] to stop the stopwatch");
				Debug.Log("Press [R] to reset the stopwatch");
			};
			stopwatchTimer.OnTimerStop += () => Debug.Log("Stopwatch stopped!");
			stopwatchTimer.Start();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.T))
			{
				stopwatchTimer.Stop();
				Debug.Log($"Final Time: {(int)stopwatchTimer.GetTime()} seconds");
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				stopwatchTimer.Reset();
				Debug.Log("Stopwatch reset!");
			}

			stopwatchTimer.Tick(Time.deltaTime);

			if (stopwatchTimer.IsRunning)
			{
				Debug.Log($"Stopwatch Time: {(int)stopwatchTimer.GetTime()} seconds");
			}
		}
	}
}