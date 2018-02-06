using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextAmp : MonoBehaviour
{
	private Text textComponent;
	private string originalText;

	void Start()
	{
		textComponent = GetComponent<Text>();
		originalText = textComponent.text;
	}

	void Update()
	{
		if (GLComponent.hitComponent)
		{
			var hitName = GLComponent.hitComponent.name;
			textComponent.text = originalText.Replace(hitName, "<color=red>" + hitName + "</color>");
		}
		else
		{
			textComponent.text = originalText;
		}
	}
}
