using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class EventBridge : MonoBehaviour
	{
		[SerializeField] private Game _game;
		[SerializeField] private City _city;
		[SerializeField] private CityBuilder _cityBuilder;
		[SerializeField] private GridBuilder _gridBuilder;
		[SerializeField] private MenuUI _menuUI;

		public static EventBridge Instance { get; private set; }

		private void Awake()
		{
			Instance = Instance ?? this;
		}

		public void SetPowerText(int power) => _menuUI.SetPower(power);
		public void StartBuild() => _city.StartBuild();
		public void StopBuild() => _city.StopBuild();
	}
}