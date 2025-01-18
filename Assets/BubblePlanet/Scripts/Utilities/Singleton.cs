using UnityEngine;
namespace Utilities
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _reference;

		public static bool IsValid
		{
			get
			{
				return _reference != null;
			}
		}

		public bool isDontDestroy;
		
		public static T Instance
		{
			get
			{
				if (!IsValid)
				{
					_reference = FindAnyObjectByType<T>();
					if (!IsValid)
					{
						var go = new GameObject($"{typeof(T).Name}[Singleton]");
						_reference = go.AddComponent<T>();
					}
				}
				return _reference;
			}
		}

		protected virtual void Awake()
		{
			if (IsValid && _reference != this)
			{
				Destroy(gameObject);
				Debug.Log("Destroy");
			}
			else
			{
				_reference = (T)(MonoBehaviour)this;

				if (isDontDestroy)
				{
					DontDestroyOnLoad(gameObject);
				}
			}
		}

		protected virtual void OnDestroy()
		{
			_reference = null;
			Destroy(gameObject);
		}
	}
}