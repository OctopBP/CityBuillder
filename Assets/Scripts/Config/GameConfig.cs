using System;

namespace Octop.CityBuilderGame
{
	[Serializable]
	public class GameConfig
	{
		public int citySizeX;
		public int citySizeY;
		public float cellSize;
		public BuildingConfig[] buildings;
	}
}