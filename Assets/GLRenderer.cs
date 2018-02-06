using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GLRenderer : MonoBehaviour
{
	private Camera camera;
	private GLComponent[] glComponents;
	private bool mouseDragging;

	public Transform draggingPlane;
	public Material material;

	private void Awake()
	{
		camera = GetComponent<Camera>();
		glComponents = FindObjectsOfType<GLComponent>();
	}

	private void Update()
	{
		if (!mouseDragging && Input.GetMouseButton(0))
		{
			StartCoroutine(MouseDrag());
		}
	}

	private IEnumerator MouseDrag()
	{
		var hitComponent = GLComponent.hitComponent;
		if (!hitComponent || !hitComponent.dragable)
		{
			yield break;
		}

		mouseDragging = true;

		var mousePosition = Input.mousePosition;
		while (Input.GetMouseButton(0))
		{
			if (Vector3.Magnitude(mousePosition - Input.mousePosition) > 3.0f)
			{
				break;
			}
			yield return null;
		}

		while (Input.GetMouseButton(0))
		{
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			float t = Intersection.RayToPlane(ray, draggingPlane.position, draggingPlane.up);
			Debug.DrawLine(ray.origin, ray.origin + t * ray.direction, Color.white);

			hitComponent.transform.position = ray.origin + t * ray.direction;
			yield return null;
		}

		mouseDragging = false;
	}

	private void OnPostRender()
	{
		material.SetPass(0);

		if (!mouseDragging)
		{
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			GLComponent.hitComponent = null;
			foreach (var glComponent in glComponents)
			{
				if (glComponent.HitTest(ray))
				{
					GLComponent.hitComponent = glComponent;
				}
			}
		}

		foreach (var glComponent in glComponents)
		{
			glComponent.Draw();
		}
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			OnPostRender();
		}
	}
}
