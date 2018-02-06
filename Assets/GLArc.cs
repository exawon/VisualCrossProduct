using System.Collections.Generic;
using UnityEngine;

public class GLArc : GLComponent
{
	public Color lineColor;
	public Color fillColor;
	public Transform lhs;
	public Transform rhs;

	private List<Vector3> glLinestrip = new List<Vector3>();
	private List<Vector3> glTriangles = new List<Vector3>();

	private void Update()
	{
		var lhsNormalized = lhs.localPosition.normalized;
		var rhsNormalized = rhs.localPosition.normalized;
		var crossProduct = Vector3.Cross(lhsNormalized, rhsNormalized);
		var dotProduct = Vector3.Dot(lhsNormalized, rhsNormalized);
		var arc = lhsNormalized;
		var angle = Mathf.Rad2Deg * Mathf.Acos(dotProduct);
		var segments = 12;
		var rotation = Quaternion.AngleAxis(angle / segments, crossProduct);

		glLinestrip.Clear();
		for (var i = 0; i <= segments; i++)
		{
			glLinestrip.Add(arc);
			arc = rotation * arc;
		}

		glTriangles.Clear();
		for (var i = 1; i < glLinestrip.Count; i++)
		{
			glTriangles.Add(Vector3.zero);
			glTriangles.Add(glLinestrip[i - 1]);
			glTriangles.Add(glLinestrip[i]);
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

		GL.Begin(GL.LINE_STRIP);
		GL.Color(isHit ? hitColor : lineColor);
		foreach (var vertex in glLinestrip)
		{
			GL.Vertex(vertex);
		}
		GL.End();

		GL.Begin(GL.TRIANGLES);
		GL.Color(isHit ? hitColor : fillColor);
		foreach (var vertex in glTriangles)
		{
			GL.Vertex(vertex);
		}
		GL.End();

		GL.PopMatrix();
	}
}
