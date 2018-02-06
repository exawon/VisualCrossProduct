using UnityEngine;

[ExecuteInEditMode]
public class CrossProduct : MonoBehaviour
{
	public Transform lhs;
	public Transform rhs;

	void Update()
	{
		transform.localPosition = Vector3.Cross(lhs.localPosition, rhs.localPosition);
	}
}
