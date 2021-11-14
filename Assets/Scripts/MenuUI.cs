using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Octop.CityBuilderGame
{
	public class MenuUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text _powerText;
		[SerializeField] private Button _buildButton;

		private void Start()
		{
			_buildButton.onClick.AddListener(EventBridge.Instance.StartBuild);
		}

		public void SetPower(int power) => _powerText.SetText($"{power}");
	}
}