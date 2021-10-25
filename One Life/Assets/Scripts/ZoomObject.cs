using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomObject : MonoBehaviour
{
	public float smoothness = 2.3f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy)
		{
			Vector3 scale = new Vector3(1,1,1);
			
			this.transform.localScale  = Vector3.Lerp(this.transform.localScale, scale, smoothness * Time.deltaTime);
		}
    }
	
	void OnEnable()
	{
		this.transform.localScale = Vector3.zero;
	}
}
