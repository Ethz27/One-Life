using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{
   	[Header("Settings")]
	public float defaultSpeed = 10f;
	
	[Header("Jump Settings")]
	public float jumpForce = 10f;
	public float extraJumps = 1f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float groundRadius;
	float currentJumpsLeft;
	bool isGrounded;
	
	[Header("Surface Settings")]
	public float rotateWithGroundSize = 5f;
	public float rotateOnSurfaceSmoothness;
	
	[Header("Wall Jump Settings")]
	public float wallJumpTime = 0.2f;
	public float wallSlideSpeed = 0.3f;
	public Vector2 wallCheckSize;
	public Transform wallCheckPoint;
	public LayerMask whatIsWall;
	public float wallJumpSpeed = 5f;
	public float changingWallSpeed = 0.5f;
	
	public UnityEvent onDeath;
	
	
	bool isTouchingWall;
	bool isWalllSliding;
	float jumpTime;
	bool justGrounded;
	
	bool dead;
	
	//[Space(5)]
	
	//Priv var
	Rigidbody2D rb;
	float speed;
	float moveInput;
	bool isFacingRight = true;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		speed = defaultSpeed;
	}
	
	void Update()
	{
		if(dead) return;
		Jump();
		InputLogic();
		RotateWithSurface();
	}
	
	void FixedUpdate()
	{
		if(dead) return;
		CheckWorld();
		FlipPlayer();
		Move();
		WallJumpLogic();
	}
	
	void InputLogic()
	{
		moveInput = Input.GetAxis("Horizontal");
	}
	
	void Move()
	{
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
	}
	
	void CheckWorld()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		
		isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, whatIsWall);
	}
	
	void FlipPlayer()
	{
		if(!isFacingRight && moveInput > 0)
		{
			Flip();
		}
		else if(isFacingRight && moveInput < 0)
		{
			Flip();
		}
	}
	
	void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 scaler = transform.localScale;
		scaler.x *= -1;
		transform.localScale = scaler;
	}
	
	void Jump()
	{
		if(isGrounded) currentJumpsLeft = extraJumps;
		
		if(isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
		else if(!isGrounded && currentJumpsLeft > 0 && Input.GetButtonDown("Jump"))
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			currentJumpsLeft--;
			GameObject obj = ObjectPool.instance.GetPooledObject("DoubleJumpEffect");
			obj.transform.position = groundCheck.position;
			obj.transform.rotation = groundCheck.rotation;
			obj.SetActive(true);
			var emitParams = new ParticleSystem.EmitParams();
			obj.GetComponent<ParticleSystem>().Emit(emitParams, 10);
		}
		
		if(isGrounded && !justGrounded)
		{
			justGrounded = true;
			GameObject obj = ObjectPool.instance.GetPooledObject("GroundedEffect");
			obj.transform.position = groundCheck.position;
			obj.transform.rotation = groundCheck.rotation;
			obj.SetActive(true);
			var emitParams = new ParticleSystem.EmitParams();
			obj.GetComponent<ParticleSystem>().Emit(emitParams, 10);
		}
		else if(!isGrounded)
		{
			justGrounded = false;
		}
	}
	
	void WallJumpLogic()
	{
		if(isTouchingWall && !isGrounded && moveInput != 0)
		{
			isWalllSliding = true;
			jumpTime = Time.time + wallJumpTime;
		}
		else if(jumpTime < Time.time)
		{
			isWalllSliding = false;
		}
		
		if(isWalllSliding)
		{
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
			currentJumpsLeft = 0;
		}
		
		if(isWalllSliding && !isTouchingWall)
		{
			rb.velocity = new Vector2(moveInput * wallJumpSpeed, jumpForce);
		}
		
		if(isWalllSliding)
		{
			speed = changingWallSpeed;
		}
		else
		{
			speed = defaultSpeed;
		}
	}
	
	void RotateWithSurface()
	{
		 RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -groundCheck.transform.up, rotateWithGroundSize, whatIsGround);
		 
		 if(hit.collider != null)
		 {
			 Quaternion targetRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
			 transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateOnSurfaceSmoothness * Time.deltaTime);
		 }
		 else
		 {
			 Quaternion targetRot = Quaternion.FromToRotation(Vector3.forward, Vector3.zero);
			 transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateOnSurfaceSmoothness * Time.deltaTime);
		 }
		
		
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(groundCheck.position, groundRadius);
		
		Gizmos.color = Color.red;
		Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
	}
	
	public bool IsFacingRight()
	{
		return isFacingRight;
	}
	
	public void Dead()
	{
		dead = true;
		speed = 0;
		onDeath.Invoke();
	}
	
	public bool IsDead()
	{
		return dead;
	}
}
