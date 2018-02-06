using System.Collections.Generic;
using UnityEngine;

public class GLCone : GLComponent
{
	public Color color;

	private List<Vector3> glTriangles = new List<Vector3>();

	private void Update()
	{
		var vertex = transform.localPosition;
		var center = -vertex.normalized;
		var perpendicular = Intersection.Perpendicular(vertex);
		var arrow = center + 0.5f * perpendicular.normalized;
		var segments = 12;
		var rotation = Quaternion.AngleAxis(360.0f / segments, vertex);
		var glLinestrip = new List<Vector3>();
		for (var i = 0; i <= segments; i++)
		{
			glLinestrip.Add(arrow);
			arrow = rotation * arrow;
		}

		glTriangles.Clear();
		for (var i = 1; i < glLinestrip.Count; i++)
		{
			glTriangles.Add(Vector3.zero);
			glTriangles.Add(glLinestrip[i - 1]);
			glTriangles.Add(glLinestrip[i]);

			glTriangles.Add(glLinestrip[i]);
			glTriangles.Add(glLinestrip[i - 1]);
			glTriangles.Add(center);
		}
	}

	public override bool HitTest(Ray ray)
	{
		ray.origin = transform.InverseTransformPoint(ray.origin);
		ray.direction = transform.InverseTransformDirection(ray.direction);
		for (var i = 0; i < glTriangles.Count; i += 3)
		{
			if (Intersection.RayToTriangle(ray, glTriangles[i], glTriangles[i + 1], glTriangles[i + 2]) > 0.0f)
			{
				return true;
			}
		}
		return false;
	}

	public override void Draw()
	{
		GL.PushMatrix();
		GL.MultMatrix(transform.localToWorldMatrix);

		GL.Begin(GL.TRIANGLES);
		GL.Color(isHit ? hitColor : color);
		foreach (var vertex in glTriangles)
		{
			GL.Vertex(vertex);
		}
		GL.End();

		GL.PopMatrix();
	}
}
