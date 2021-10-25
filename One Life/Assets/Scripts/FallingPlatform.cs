using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
	public GameObject breakParticle;
	public GameObject[] platformGraphic;
	public float delay = 0.5f;
	public float resetDelay = 3f;
	
	bool _done;
	BoxCollider2D col;
	
    // Start is called before the first frame update
    void Start()
    {
		breakParticle.SetActive(false);
		col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	void OnCollisionEnter2D(Collision2D collision)
	{			
		if(collision.transform.gameObject.tag == "Player" && !_done)
		{
			_done = true;
			Invoke("PlatformBreak", delay);
		}
	}
	
	void PlatformBreak()
	{
		breakParticle.SetActive(true);
		
		foreach(GameObject obj in platformGraphic)
		{
			obj.SetActive(false);
		}
		
		col.enabled = false;
		
		Invoke("ResetPlatform", resetDelay);
	}
	
	void ResetPlatform()
	{
		foreach(GameObject obj in platformGraphic)
		{
			obj.SetActive(true);
		}
		_done = false;
		breakParticle.SetActive(false);
		col.enabled = true;
	}
	
}
