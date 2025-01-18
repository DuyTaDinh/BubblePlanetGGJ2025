using UnityEngine;
using UnityEngine.UI;
namespace UI
{
	public class ImageScrollEffect : MonoBehaviour
	{
		RawImage _image;
		[SerializeField] Vector2 speed = new Vector2(0.02f, 0.02f);

		void Start()
		{
			_image = GetComponent<RawImage>();
			if (_image == null)
			{
				Debug.LogError("RawImage component not found!");
			}
		}

		void Update()
		{
			Vector2 offset = _image.uvRect.position;
			offset += speed * Time.deltaTime;
			_image.uvRect = new Rect(offset, _image.uvRect.size);
		}

	}
}