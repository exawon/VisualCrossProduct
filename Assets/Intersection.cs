using UnityEngine;

public static class Intersection
{
	public static Vector3 Perpendicular(Vector3 v)
	{
		if (v.x * v.x > v.y * v.y)
		{
			if (v.x * v.x > v.z * v.z)
			{
				return new Vector3(-(v.y + v.z) / v.x, 1.0f, 1.0f);
			}
		}
		else if (v.y * v.y > v.z * v.z)
		{
			return new Vector3(1.0f, -(v.x + v.z) / v.y, 1.0f);
		}

		return new Vector3(1.0f, 1.0f, -(v.x + v.y) / v.z);
	}

	public static float RayToLine(Ray ray, Vector3 p0, Vector3 p1)
	{
		var a = ray.direction;
		var b = ray.origin - p0;
		var c = p1 - p0;

		var p = Vector3.Cross(a, b);
		var d = Vector3.Dot(p, c);
		if (d < -1.0f || d > 1.0f)
		{
			return 0.0f;
		}

		var q = Vector3.Cross(a, c);
		var s = Vector3.Dot(p, q) / q.sqrMagnitude;
		if (s < 0.0f || s > 1.0f)
		{
			return 0.0f;
		}

		return (p0 - ray.origin + s * c).magnitude;
	}

	public static float RayToTriangle(Ray ray, Vector3 p0, Vector3 p1, Vector3 p2)
	{
		var e1 = p1 - p0;
		var e2 = p2 - p0;
		var p = Vector3.Cross(ray.direction, e2);
		var d = Vector3.Dot(e1, p);
		if (d > -float.Epsilon && d < float.Epsilon)
		{
			return 0.0f;
		}

		var f = 1.0f / d;
		var s = ray.origin - p0;
		var u = f * Vector3.Dot(s, p);
		if (u < 0.0f || u > 1.0f)
		{
			return 0.0f;
		}

		var q = Vector3.Cross(s, e1);
		var v = f * Vector3.Dot(ray.direction, q);
		if (v < 0.0f || u + v > 1.0f)
		{
			return 0.0f;
		}

		return f * Vector3.Dot(e2, q);
	}

	public static float RayToPlane(Ray ray, Vector3 p, Vector3 normal)
	{
		var d = Vector3.Dot(normal, ray.direction);
		if (d > -float.Epsilon && d < float.Epsilon)
		{
			return 0.0f;
		}

		return Vector3.Dot(normal, p - ray.origin) / Vector3.Dot(normal, ray.direction);
	}
}
