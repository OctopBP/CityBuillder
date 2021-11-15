using TMPro;
using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class Building : MonoBehaviour
	{
		[SerializeField] private Transform _model;
		[SerializeField] private TMP_Text _powerText;
		[SerializeField] private float _textOffset = 1;

		[Header("View")]
		[SerializeField] private Renderer _modelRenderer;
		[SerializeField] private Material _defaultMaterial;
		[SerializeField] private Material _transparentMaterial;

		public Vector2Int Location { get; private set; }
		public Vector2Int Size => new Vector2Int(Config.sizeX, Config.sizeY);
		public BuildingConfig Config { get; private set; }

		public void Init(GameConfig gameConfig, BuildingConfig buildingConfig)
		{
			Config = buildingConfig;

			_model.localScale = new Vector3(buildingConfig.sizeX, buildingConfig.height, buildingConfig.sizeY) * gameConfig.cellSize;

			_powerText.SetText($"{buildingConfig.power}");
			_powerText.transform.localPosition = new Vector3(
				x: buildingConfig.sizeX * gameConfig.cellSize / 2,
				y: buildingConfig.height * gameConfig.cellSize + _textOffset,
				z: buildingConfig.sizeY * gameConfig.cellSize / 2
			);
		}

		public void SetupFrom(Building building)
		{
			SetConfig(building.Config);
			SetLocation(building.Location);

			gameObject.SetActive(true);
			gameObject.name = $"Building {Config.power} [{Location.x}, {Location.y}]";
		}

		public void SetConfig(BuildingConfig buildingConfig) => Config = buildingConfig;
		public void SetLocation(Vector2Int location) => Location = location;
		public void SetDefault() => SetMaterial(_defaultMaterial);
		public void SetTransparant() => SetMaterial(_transparentMaterial);
		public void SetMaterial(Material material) => _modelRenderer.material = material;
	}
}