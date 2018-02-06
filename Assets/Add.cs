using UnityEngine;

[ExecuteInEditMode]
public class Add : MonoBehaviour
{
	public Transform lhs;
	public Transform rhs;

	void Update()
	{
		transform.localPosition = lhs.localPosition + rhs.localPosition;
	}
}
