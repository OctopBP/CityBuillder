using System.Collections.Generic;
using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class GridView : MonoBehaviour
	{
		[SerializeField] private Renderer _renderer;
		[SerializeField] private Material _gridMaterial;
		[SerializeField] private Material _defaultMaterial;

		private Texture2D _texture;

		public void Init(GameConfig gameConfig)
		{
			int sizeX = gameConfig.citySizeX;
			int sizeY = gameConfig.citySizeY;

			_gridMaterial.SetFloatArray("_Cells", new float[sizeX * sizeY]);
			_gridMaterial.SetVector("_Size", new Vector4(sizeX, sizeY, 0, 0));

			_texture = new Texture2D(sizeX, sizeY, TextureFormat.R16, 0, true);
			_texture.filterMode = FilterMode.Point;

			transform.localScale = new Vector3(sizeX, sizeY, 0) * gameConfig.cellSize;
		}

		public void UpdateCells(List<Vector2Int> cellsData, List<Vector2Int> occupiedCells)
		{
			for (int i = 0; i < _texture.width; i++)
			{
				for (int j = 0; j < _texture.height; j++)
				{
					Vector2Int cell = cellsData[i * _texture.height + j];
					bool isOccupied = occupiedCells.Contains(cell);
					_texture.SetPixel(i, j, isOccupied ? Color.white : Color.black);
				}
			}

			_texture.Apply();
			_gridMaterial.SetTexture("_GridMap", _texture);
		}

		public void SetGridMaterial() => _renderer.material = _gridMaterial;
		public void SetDefaultMaterial() => _renderer.material = _defaultMaterial;
	}
}