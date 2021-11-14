using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class City : MonoBehaviour
	{
		[SerializeField] private GridBuilder _gridBuilder;
		[SerializeField] private CityBuilder _cityBuilder;

		[SerializeField] private Transform _buildingContainer;

		private GameConfig _gameConfig;
		private List<Building> _buildings = new List<Building>();
		private bool _buildingActive;

		public void Init(InputController inputController, GameConfig gameConfig)
		{
			_gameConfig = gameConfig;

			_gridBuilder.Init(gameConfig);
			_cityBuilder.Init(inputController, gameConfig);

			_cityBuilder.OnPreviewMove += OnPreviewMove;
			_cityBuilder.OnTryBuild += TryBuild;
		}

		private void OnDestroy()
		{
			_cityBuilder.OnPreviewMove -= OnPreviewMove;
			_cityBuilder.OnTryBuild -= TryBuild;
		}

		public void StartBuild()
		{
			if (_buildingActive) return;
			_buildingActive = true;

			_cityBuilder.StartBuilding();

			foreach (Building building in _buildings)
				building.SetTransparant();
		}

		public void StopBuild()
		{
			if (!_buildingActive) return;
			_buildingActive = false;

			_cityBuilder.StopPreview();
			_gridBuilder.ResetCells();

			foreach (Building building in _buildings)
				building.SetDefault();
		}

		private void TryBuild(Building building)
		{
			bool canBuild = _gridBuilder.CanBuild(building, _buildings);
			if (!canBuild)
				return;

			AddNewBuilding(building);
			CalculatePower();
			StopBuild();
		}

		private void AddNewBuilding(Building building)
		{
			Building newBuilding = Instantiate(building, _buildingContainer);
			newBuilding.SetupFrom(building);
			_buildings.Add(newBuilding);
			_gridBuilder.CalculateOccupiedCells(_buildings);
		}

		private void CalculatePower()
		{
			int powerSum = _buildings.Sum(b => b.Config.power);
			EventBridge.Instance.SetPowerText(powerSum);
		}

		private void OnPreviewMove(Building building)
		{
			bool canBuild = _gridBuilder.CanBuildAndUpdateCells(building, _buildings);
			_cityBuilder.CanBuild(canBuild);
		}
	}
}