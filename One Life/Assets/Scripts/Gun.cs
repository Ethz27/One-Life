using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;
	public Transform gunGraphic;
	public Transform gunPoint;
	public string bullet;
	public float bulletSpeed = 10f;
	public float fireRate = 0.1f;
	
	Vector2 dir;
	float timeTillNextShot;
	// Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
		Twist();
		
		if(Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
    }
	
	void FaceMouse()
	{
		Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		dir = mousePos - (Vector2)gunGraphic.position;
		dir.x = Mathf.Clamp(dir.x, 15, -15);
		gunGraphic.transform.right = dir;
	}
	
	void Shoot()
	{
		GameObject obj = ObjectPool.instance.GetPooledObject(bullet);
		obj.SetActive(true);
		obj.transform.position = gunPoint.position;
		obj.transform.rotation = gunPoint.rotation;
		obj.transform.GetComponent<Rigidbody2D>().AddForce(obj.transform.right * bulletSpeed);
	}
	
	void Twist()
	{
		PlayerMovement pm = GetComponentInParent<PlayerMovement>();
		if(pm.IsFacingRight())
		{
			Vector3 angle = new Vector3(gunPoint.transform.localRotation.eulerAngles.x, gunPoint.transform.localRotation.eulerAngles.y, 0);
			gunPoint.transform.rotation = Quaternion.Euler(angle);
		}
		else
		{
			Vector3 angle = new Vector3(gunPoint.transform.localRotation.eulerAngles.x, gunPoint.transform.localRotation.eulerAngles.y, 180);
			gunPoint.transform.rotation = Quaternion.Euler(angle);
		}
	}
}
