using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRise : MonoBehaviour
{
	public float endHeight;
	public float speed = 0.5f;
	public Color red;
	public Color orange;
	public float changingColourSpeed = 0.5f;
	
	Color lerpedColor = Color.white;
	SpriteRenderer sr;
	
	// Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, Mathf.SmoothStep(transform.localScale.y, endHeight, speed * Time.deltaTime), transform.localScale.z);
		
		lerpedColor = Color.Lerp(orange, red, Mathf.PingPong(Time.time, changingColourSpeed));
		sr.color = lerpedColor;
    }
}
