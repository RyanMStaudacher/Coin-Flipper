using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FadeText : MonoBehaviour
{
    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private float fadeSpeed = 1.0f;

    private Text text;

	// Use this for initialization
	void Start ()
    {
		if(GetComponent<Text>() != null)
        {
            text = GetComponent<Text>();
        }

        StartCoroutine(FadeTextTimer());
	}

    IEnumerator FadeTextTimer()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(FadeTheText());
    }

    IEnumerator FadeTheText()
    {
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadeSpeed));
            yield return null;
        }
    }
}
