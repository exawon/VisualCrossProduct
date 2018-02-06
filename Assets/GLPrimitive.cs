using System.Collections.Generic;
using UnityEngine;

public class GLPrimitive : GLComponent
{
	public Color color;

	public enum Topology
	{
		TRIANGLES = 4,
		TRIANGLE_STRIP = 5,
		QUADS = 7,
		LINES = 1,
		LINE_STRIP = 2,
	}

	public Topology topology;
	public List<Transform> vertices;

	private List<Vector3> glVertices = new List<Vector3>();

	private void Update()
	{
		glVertices.Clear();
		foreach (var vertex in vertices)
		{
			glVertices.Add(vertex.position);
		}
	}

	public override bool HitTest(Ray ray)
	{
		switch (topology)
		{
		case Topology.TRIANGLES:
			for (var i = 0; i < glVertices.Count; i += 3)
			{
				if (Intersection.RayToTriangle(ray, glVertices[i], glVertices[i + 1], glVertices[i + 2]) > 0.0f)
				{
					return true;
				}
			}
			break;

		case Topology.TRIANGLE_STRIP:
			for (var i = 2; i < glVertices.Count; i++)
			{
				if (Intersection.RayToTriangle(ray, glVertices[i - 2], glVertices[i - 1], glVertices[i]) > 0.0f)
				{
					return true;
				}
			}
			break;

		case Topology.QUADS:
			for (var i = 0; i < glVertices.Count; i += 4)
			{
				if (Intersection.RayToTriangle(ray, glVertices[i], glVertices[i + 1], glVertices[i + 2]) > 0.0f
				|| Intersection.RayToTriangle(ray, glVertices[i], glVertices[i + 3], glVertices[i + 2]) > 0.0f)
				{
					return true;
				}
			}
			break;

		case Topology.LINES:
			for (var i = 0; i < glVertices.Count; i += 2)
			{
				if (Intersection.RayToLine(ray, glVertices[i], glVertices[i + 1]) > 0.0f)
				{
					return true;
				}
			}
			break;

		case Topology.LINE_STRIP:
			for (var i = 1; i < glVertices.Count; i++)
			{
				if (Intersection.RayToLine(ray, glVertices[i - 1], glVertices[i]) > 0.0f)
				{
					return true;
				}
			}
			break;
		}
		return false;
	}

	public override void Draw()
	{
		GL.Begin((int)topology);
		GL.Color(isHit ? hitColor : color);
		foreach (var vertex in glVertices)
		{
			GL.Vertex(vertex);
		}
		GL.End();
	}
}
