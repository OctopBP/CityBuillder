using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class GameConfigParcer
	{
		public GameConfig Parse(string json)
		{
			return JsonUtility.FromJson<GameConfig>(json);
		}
	}
}