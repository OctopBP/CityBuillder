using System.Collections.Generic;
using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class GridBuilder : MonoBehaviour
	{
		[SerializeField] private GridView _gridView;
		[SerializeField] private int _gap = 1;

		private GameConfig _gameConfig;
		private List<Vector2Int> _cells = new List<Vector2Int>();
		private List<Vector2Int> _occupiedCells = new List<Vector2Int>();

		public void Init(GameConfig gameConfig)
		{
			_gameConfig = gameConfig;

			_gridView.Init(_gameConfig);

			Build();
		}

		public void UpdateCells(List<Building> buildings)
		{
			float[] cellsData = new float[_cells.Count];

			for (int i = 0; i < _cells.Count; i++)
			{
				if (_occupiedCells.Contains(_cells[i]))
					cellsData[i] = 1;
				else
					cellsData[i] = 0;
			}

			_gridView.SetGridMaterial();
			_gridView.UpdateCells(cellsData);
		}

		public void CalculateOccupiedCells(List<Building> buildings)
		{
			_occupiedCells = new List<Vector2Int>();
			foreach (Building building in buildings)
			{
				for (int x = 0; x < building.Size.x; x++)
				{
					for (int y = 0; y < building.Size.y; y++)
					{
						for (int i = -_gap; i <= _gap; i++)
						{
							for (int j = -_gap; j <= _gap; j++)
							{
								Vector2Int cellLocation = building.Location + new Vector2Int(x + i, y + j);
								if (!_occupiedCells.Contains(cellLocation))
									_occupiedCells.Add(cellLocation);
							}
						}
					}
				}
			}
		}

		public bool CanBuildAndUpdateCells(Building building, List<Building> buildings)
		{
			bool canBuild = CanBuild(building, buildings);
			UpdateCells(buildings);
			return canBuild;
		}

		public bool CanBuild(Building building, List<Building> buildings)
		{
			for (int x = 0; x < building.Size.x; x++)
			{
				for (int y = 0; y < building.Size.y; y++)
				{
					Vector2Int cellLocation = building.Location + new Vector2Int(x, y);
					if (_occupiedCells.Contains(cellLocation))
						return false;
				}
			}

			for (int x = 0; x < building.Size.x; x++)
			{
				for (int y = 0; y < building.Size.y; y++)
				{
					Vector2Int cellLocation = building.Location + new Vector2Int(x, y);
					if (cellLocation.x < 0) return false;
					if (cellLocation.y < 0) return false;
					if (cellLocation.x >= _gameConfig.citySizeX) return false;
					if (cellLocation.y >= _gameConfig.citySizeY) return false;
				}
			}

			return true;
		}

		public void ResetCells()
		{
			_gridView.SetDefaultMaterial();
		}

		private void Build()
		{
			transform.position = new Vector3(-_gameConfig.citySizeX / 2, 0, -_gameConfig.citySizeY / 2) * _gameConfig.cellSize;

			_cells.Capacity = _gameConfig.citySizeX * _gameConfig.citySizeY;

			for (int x = 0; x < _gameConfig.citySizeX; x++)
			{
				for (int y = 0; y < _gameConfig.citySizeY; y++)
				{
					_cells.Add(new Vector2Int(x, y));
				}
			}
		}
	}
}