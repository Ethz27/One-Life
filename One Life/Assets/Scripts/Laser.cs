using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public Transform firePoint;
	public Transform firePointHolder;
	LineRenderer lineRend;
	
	public bool rotate = true;
	public float speed = 5f;
	public float maxRot = 1f;
	
	public bool turnOff = true;
	public float startDelay = 1f;
	public float delay = 5f;
	public float resetDelay = 2f;
	bool _done;
	bool laserActive = true;
	
	// Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
		lineRend.enabled = false;
		
		Invoke("ActivateLaser", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
       if(!laserActive) return;
	   RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.up);
		lineRend.SetPosition(0, firePoint.position);
		lineRend.SetPosition(1, hit.point);
		lineRend.enabled = true;
		
		if(hit.transform.gameObject.tag == "Player")
		{
			hit.transform.gameObject.GetComponent<PlayerMovement>().Dead();
		}
		
		if(rotate) firePointHolder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.PingPong(Time.time * speed, maxRot));
		
		if(turnOff && !_done)
		{
			_done = true;
			Invoke("DeactivateLaser", delay);
		}
	}
	
	void DeactivateLaser()
	{
		lineRend.enabled = false;
		laserActive = false;
		Invoke("ActivateLaser", resetDelay);
	}
	
	void ActivateLaser()
	{
		_done = false;
		lineRend.enabled = true;
		laserActive = true;
	}
	
}
