using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private InputController _inputController;
		[SerializeField] private City _city;
		[SerializeField] private TextAsset _jsonConfig;

		private void Start()
		{
			GameConfig gameConfig = new GameConfigParcer().Parse(_jsonConfig.text);
			_city.Init(_inputController, gameConfig);
		}
	}
}