using UnityEngine;

[ExecuteInEditMode]
public class DotProduct : MonoBehaviour
{
	public Transform lhs;
	public Transform rhs;

	void Update()
	{
		var rhsNormalized = rhs.localPosition.normalized;
		transform.localPosition = rhsNormalized * Vector3.Dot(lhs.localPosition, rhsNormalized);
	}
}
