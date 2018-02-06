using UnityEngine;

public class GLPerpendicular : GLComponent
{
	public Color lineColor;
	public Color fillColor;
	public Transform lhs;
	public Transform rhs;

	public override void Draw()
	{
		var lhsNormalized = lhs.localPosition.normalized;
		var rhsNormalized = rhs.localPosition.normalized;

		GL.PushMatrix();
		GL.MultMatrix(transform.localToWorldMatrix);

		GL.Begin(GL.LINE_STRIP);
		GL.Color(lineColor);
		GL.Vertex(lhsNormalized);
		GL.Vertex(lhsNormalized + rhsNormalized);
		GL.Vertex(rhsNormalized);
		GL.End();

		GL.Begin(GL.QUADS);
		GL.Color(fillColor);
		GL.Vertex(Vector3.zero);
		GL.Vertex(lhsNormalized);
		GL.Vertex(lhsNormalized + rhsNormalized);
		GL.Vertex(rhsNormalized);
		GL.End();

		GL.PopMatrix();
	}
}
