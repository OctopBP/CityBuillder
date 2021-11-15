using UnityEngine;

namespace Octop.CityBuilderGame
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private InputController _inputController;
		[SerializeField] private float _rotateSpeed = 100;

		private Quaternion _targetRotation;

		private void Start()
		{
			_inputController.OnDrag += Drag;
		}

		private void Update()
		{
			RotationHandler();
		}

		private void OnDestroy()
		{
			_inputController.OnDrag -= Drag;
		}

		private void Drag(Vector3 position, Vector3 delta)
		{
			transform.position += delta;
		}

		private void RotationHandler()
		{
			if (Input.GetKey(KeyCode.RightArrow))
				_targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * _rotateSpeed * Time.deltaTime);

			if (Input.GetKey(KeyCode.LeftArrow))
				_targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * -_rotateSpeed * Time.deltaTime);

			transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 0.5f);
		}
	}
}