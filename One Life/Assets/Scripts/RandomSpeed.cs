using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeed : MonoBehaviour
{
    public int minJump = 2;
	public int maxJump = 6;
	int currentJumpHeight;
	PlayerMovement pm;
	
	// Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
		StartCoroutine("DoCheck");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ChangeJumpHeight()
	{
		currentJumpHeight = Random.Range(minJump, maxJump);
		pm.jumpForce = currentJumpHeight;
	}
	
	IEnumerator DoCheck() {
     for(;;) {
         // execute block of code here
		 ChangeJumpHeight();
         yield return new WaitForSeconds(10f);
     }
 }
}
