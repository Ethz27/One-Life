using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroToLevel : MonoBehaviour
{
	public Transform player;
	public CinemachineVirtualCamera cam; 
	
   public float minimum;
   public float maximum;
   public float duration;
   
   public float introZoom;
   public float playerZoom = 3.6f;
   public float changeZoomSmoothness = 2.4f;
   
   public Vector2 posToStickTo;
   
   public GameObject lava;
   
   float startTime;
   bool changeZoom;

   // Start is called before the first frame update
    void Start()
    {
		cam.m_Follow = this.transform;
		cam.m_LookAt = this.transform;
		
		startTime = Time.time;
		cam.m_Lens.OrthographicSize = introZoom;
		lava.SetActive(false);
		
		Invoke("ChangeCam", duration);
    }

    // Update is called once per frame
    void Update()
    {
		float t = (Time.time - startTime) / duration;
        transform.position = new Vector3(posToStickTo.x, Mathf.SmoothStep(minimum, maximum, t), posToStickTo.y);
		
		if(changeZoom) cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, playerZoom, changeZoomSmoothness * Time.deltaTime);
    }
	
	void ChangeCam()
	{
		changeZoom = true;
		cam.m_Follow = player;
		cam.m_LookAt = player;
		Invoke("StartGame", duration);
	}
	
	public void StartGame()
	{
		lava.SetActive(true);
		this.transform.gameObject.SetActive(false);
	}
}
