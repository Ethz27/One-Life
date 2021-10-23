using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnCollisionEnter2D(Collision2D collision)
	{			
		if(collision.transform.gameObject.tag == "Player")
		{
			collision.transform.gameObject.GetComponent<PlayerMovement>().Dead();
		}
	}
}
