using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI
{
	public class SceneLoader : MonoBehaviour
	{
		public void NextScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}