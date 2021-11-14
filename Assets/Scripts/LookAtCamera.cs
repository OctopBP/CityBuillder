using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class LookAtCamera : MonoBehaviour
	{
		private Transform _cameraTransform;

		private void Start()
		{
			_cameraTransform = Camera.main.transform;
		}

		private void Update()
		{
			if (_cameraTransform != null)
				transform.LookAt(_cameraTransform);
		}
	}
}