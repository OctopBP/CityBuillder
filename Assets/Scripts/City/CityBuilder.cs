using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Octop.CityBuilderGame
{
	public class CityBuilder : MonoBehaviour
	{
		[SerializeField] private Building _buildingPrefab;
		[SerializeField] private GridBuilder _gridBuilder;

		[Header("View")]
		[SerializeField] private Material _normalMaterial;
		[SerializeField] private Material _cantBuildMaterial;

		public event Action<Building> OnTryBuild;
		public event Action<Building> OnPreviewMove;

		private Building _buildingPreview;
		private InputController _inputController;
		private GameConfig _gameConfig;

		public void Init(InputController inputController, GameConfig gameConfig)
		{
			_inputController = inputController;
			_gameConfig = gameConfig;
		}

		public void StartBuilding()
		{
			BuildingConfig randomConfig = GetBuildingConfig();
			SetupPreviewBuilding(randomConfig);

			_inputController.OnMouseMove += MoveBuildingPreview;
			_inputController.OnMouseClick += TryBuild;
		}

		public void CanBuild(bool canBuild) => _buildingPreview.SetMaterial(canBuild ? _normalMaterial : _cantBuildMaterial);

		public void StopPreview()
		{
			_inputController.OnMouseMove -= MoveBuildingPreview;
			_inputController.OnMouseClick -= TryBuild;

			_buildingPreview?.gameObject.SetActive(false);
		}

		private void TryBuild(Vector3 _) => OnTryBuild?.Invoke(_buildingPreview);

		private BuildingConfig GetBuildingConfig()
		{
			int randomIndex = Random.Range(0, _gameConfig.buildings.Length);
			BuildingConfig randomConfig = _gameConfig.buildings[randomIndex];
			return randomConfig;
		}

		private void SetupPreviewBuilding(BuildingConfig randomConfig)
		{
			if (_buildingPreview == null)
				_buildingPreview = Instantiate(_buildingPrefab, this.transform);

			_buildingPreview.Init(_gameConfig, randomConfig);
			_buildingPreview.gameObject.SetActive(true);
		}

		private void MoveBuildingPreview(Vector3 hitPoint)
		{
			Vector2Int steeptPoint = new Vector2Int(
				x: RoundCoord(hitPoint.x) - RoundLocation(_buildingPreview.Size.x),
				y: RoundCoord(hitPoint.z) - RoundLocation(_buildingPreview.Size.x));

			Vector2Int offcet = new Vector2Int(RoundLocation(_gameConfig.citySizeX), RoundLocation(_gameConfig.citySizeY));
			Vector2Int location = steeptPoint + offcet;

			if (location != _buildingPreview.Location)
			{
				Vector3 targetPos = new Vector3(steeptPoint.x, 0, steeptPoint.y) * _gameConfig.cellSize;
				_buildingPreview.transform.position = targetPos;
				_buildingPreview.SetLocation(location);
				OnPreviewMove?.Invoke(_buildingPreview);
			}

			int RoundCoord(float value) => Mathf.RoundToInt(value / _gameConfig.cellSize);
			int RoundLocation(float value) => Mathf.RoundToInt(value / 2);
		}
	}
}