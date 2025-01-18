using Gameplay;
using UnityEngine;
using Utilities;
namespace Managers
{
	public class DataManager : Singleton<DataManager>
	{
		[SerializeField] private BubbleField bubbleField;
		
		
		public BubbleField BubbleField => bubbleField;
	}
}