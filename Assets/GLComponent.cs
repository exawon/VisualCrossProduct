using UnityEngine;

public class GLComponent : MonoBehaviour
{
	protected static Color hitColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);

	public static GLComponent hitComponent;

	public bool isHit
	{
		get
		{
			return hitComponent == this;
		}
	}

	public bool dragable;

	public virtual bool HitTest(Ray ray)
	{
		return false;
	}

	public virtual void Draw()
	{
	}
}
