using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public Transform player;
	public float maxDist = 10f;
	DistanceJoint2D dj;
	bool ableToActivate;
	
	// Start is called before the first frame update
    void Start()
    {
        dj = GetComponent<DistanceJoint2D>();
		dj.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(ableToActivate);

		if(Vector3.Distance(FindClosestGrapplePoint().transform.position, player.transform.position) < maxDist)
		 {
			 ableToActivate = true;
		 }
		 else
		 {
			 ableToActivate = false;
		 }
		 
		 if(ableToActivate)
		 {
			 dj.enabled = true;
			 dj.connectedAnchor = FindClosestGrapplePoint().transform.position;
		 }
		 else
		 {
			  dj.enabled = false;
		 }
    }
	
	public GameObject FindClosestGrapplePoint()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("GrapplePoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
			else
			{
				return null;
			}
        }
        return closest;
    }
}
