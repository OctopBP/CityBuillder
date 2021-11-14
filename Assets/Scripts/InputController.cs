using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Octop.CityBuilderGame
{
	public class InputController : MonoBehaviour
	{
		public event Action<Vector3, Vector3> OnDrag;
		public event Action<Vector3> OnMouseMove;
		public event Action<Vector3> OnMouseClick;

		private const float DragTresshold = 0.2f;

		private float _clickTime;
		private Vector3 _clickPosition;
		private Plane _plane;

		private void Start()
		{
			_plane = new Plane(Vector3.up, Vector3.zero);
		}

		private void Update()
		{
			HandleEscape();
			HandleMouse();
		}

		private void HandleEscape()
		{
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
				EventBridge.Instance.StopBuild();
		}

		private void HandleMouse()
		{
			Vector3 mousePosition = GetHitPoint();

			if (Input.GetMouseButtonDown(0))
			{
				_clickTime = Time.time;
				_clickPosition = mousePosition;
				return;
			}

			if (Input.GetMouseButton(0))
			{
				Vector3 delta = mousePosition - _clickPosition;
				OnDrag?.Invoke(mousePosition, delta);
				_clickPosition = mousePosition;
				return;
			}

			if (Input.GetMouseButtonUp(0))
			{
				if (EventSystem.current.IsPointerOverGameObject())
					return;

				if (Time.time - _clickTime > DragTresshold)
					return;

				OnMouseClick?.Invoke(mousePosition);
			}

			OnMouseMove?.Invoke(mousePosition);
		}

		private Vector3 GetHitPoint()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (!_plane.Raycast(ray, out float point))
				return Vector3.zero;

			return ray.GetPoint(point);
		}
	}
}