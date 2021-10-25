using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
   public GameObject finishLevelMenu;
   public GameObject[] finishLevelMenuObjects;
   public float smoothness = 2.3f;
   bool open;
   
   // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in finishLevelMenuObjects)
		{
			obj.transform.localScale = Vector3.zero;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
		{
			Vector3 scale = new Vector3(1,1,1);
			
			foreach(GameObject obj in finishLevelMenuObjects)
			{
				obj.transform.localScale  = Vector3.Lerp(obj.transform.localScale, scale, smoothness * Time.deltaTime);
			}
		}
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.gameObject.tag == "Player")
		{
			open = true;
		}
    }
}
