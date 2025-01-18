using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI
{
	public class LoadScene: MonoBehaviour
	{
		public void LoadSceneByPath(string scenePath)
		{
			SceneManager.LoadScene(scenePath);
		}
	}
}