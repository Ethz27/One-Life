using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColour : MonoBehaviour
{
	public Color First;
	public Color Second;
	public float changingColourSpeed = 0.5f;
	
	Color lerpedColor = Color.white;
	Image sr;
	
	// Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		lerpedColor = Color.Lerp(First, Second, Mathf.PingPong(Time.time, changingColourSpeed));
		sr.color = lerpedColor;
    }
}
