using TMPro;
using UnityEngine;
namespace Gameplay
{
	public class DamageTextSpawner: MonoBehaviour
	{
		public GameObject damageTextPrefab; // Assign the prefab in the Inspector
		public Transform canvasTransform;  // Assign the Canvas transform in the Inspector

		public void SpawnDamageText(Vector3 position, float amount)
		{
			// Instantiate the damage text prefab
			GameObject instance = Instantiate(damageTextPrefab, canvasTransform);

			// Convert world position to UI position
			Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
			instance.transform.position = screenPosition;

			// Set the text and color
			TMP_Text textComponent = instance.GetComponent<TMP_Text>();
			if (textComponent != null)
			{
				if (amount > 0)
				{
					textComponent.text = $"+{amount:F1} HP"; // Positive HP
					textComponent.color = Color.green;      // Green for healing
				}
				else
				{
					textComponent.text = $"{amount:F1} HP"; // Negative HP
					textComponent.color = Color.red;        // Red for damage
				}
			}

			// Destroy the object after animation ends
			Destroy(instance, 1.5f); // Adjust duration as needed
		}
	}
}