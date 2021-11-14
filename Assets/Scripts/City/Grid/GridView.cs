using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class GridView : MonoBehaviour
	{
		[SerializeField] private Renderer _renderer;
		[SerializeField] private Material _gridMaterial;
		[SerializeField] private Material _defaultMaterial;

		public void Init(GameConfig gameConfig)
		{
			int sizeX = gameConfig.citySizeX;
			int sizeY = gameConfig.citySizeY;

			_gridMaterial.SetFloatArray("_Cells", new float[sizeX * sizeY]);
			_gridMaterial.SetVector("_Size", new Vector4(sizeX, sizeY, 0, 0));

			transform.localScale = new Vector3(sizeX, sizeY, 0) * gameConfig.cellSize;
		}

		public void UpdateCells(float[] cellsData)
		{
			_gridMaterial.SetFloatArray("_Cells", cellsData);
		}

		public void SetGridMaterial() => _renderer.material = _gridMaterial;
		public void SetDefaultMaterial() => _renderer.material = _defaultMaterial;
	}
}