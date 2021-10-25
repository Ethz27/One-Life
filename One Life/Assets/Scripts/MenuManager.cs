using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[Header("Main Settings")]
	public PlayerMovement playerMovement;
	[SerializeField] KeyCode menuKey = KeyCode.Escape;
	public GameObject uiPostProcessing;
	public float smoothness = 0.4f;
	
	[Header("Pause Menu Settings")]
	public GameObject pauseMenu;
	public GameObject[] pauseMenuMain; //Scale these from 0 to 1 cuz look cool like ur mum
	
	[Header("Death Menu Settings")]
	public GameObject deathMenu;
	public GameObject[] deathMenuMain;
	
	bool menuOpen;
	bool deathMenuOpen;
	
	// Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in deathMenuMain)
		{
			obj.transform.localScale = Vector3.zero;
		}
		
		foreach(GameObject obj in pauseMenuMain)
		{
			obj.transform.localScale = Vector3.zero;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(menuKey) && !menuOpen && !playerMovement.IsDead())
		{
			OpenMenu();
		}
		else if(Input.GetKeyDown(menuKey) && menuOpen && !playerMovement.IsDead())
		{
			CloseMenu();
		}
		
		if(playerMovement.IsDead())
		{
			CloseMenu();
			OpenDeathMenu();
			menuOpen = false;
		}
		
		if(deathMenuOpen)
		{
			Vector3 scale = new Vector3(1,1,1);
			
			foreach(GameObject obj in deathMenuMain)
			{
				obj.transform.localScale  = Vector3.Lerp(obj.transform.localScale, scale, smoothness * Time.deltaTime);
			}
		}
		
		if(menuOpen)
		{
			Vector3 scale = new Vector3(1,1,1);
			
			foreach(GameObject obj in pauseMenuMain)
			{
				obj.transform.localScale  = Vector3.Lerp(obj.transform.localScale, scale, smoothness * Time.deltaTime);
			}
		}
    }
	
	void OpenMenu()
	{
		menuOpen = true;
		uiPostProcessing.SetActive(true);
		pauseMenu.SetActive(true);
	}
	
	public void CloseMenu()
	{
		menuOpen = false;
		uiPostProcessing.SetActive(false);
		pauseMenu.SetActive(false);
		
		foreach(GameObject obj in pauseMenuMain)
		{
			obj.transform.localScale = Vector3.zero;
		}
	}
	
	void OpenDeathMenu()
	{
		uiPostProcessing.SetActive(true);
		deathMenuOpen = true;
		deathMenu.SetActive(true);
	}
}
