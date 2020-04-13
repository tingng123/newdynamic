using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{

	public CharacterController2D controller;
	public Rigidbody2D rb;
	public bool facingRt = true;
	//public bool flip = true;
	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;


	private Vector3 difference;



	public bool isCrouching;
	public Transform ceiling;
	public float radiuss = 0.02f;
	public LayerMask ground;

	public bool isGrounded;
	public Transform grounder;
	public float radiuss2 = 0.02f;
	public LayerMask ground2;



	public Animator anim;




	public GameObject brick;
	public Transform throwPt;
	public float distance;
	public float height;
	public float Force;
	public float rotZ;

	public GameObject glassbottle;




	/// jump
	public float actionCooldown = 1.0f;
	public float timeSinceAction = 0.0f;

	/// run
	public float throwActionCooldown = 1.0f;
	public float timeSincethrowAction = 0.0f;

	// throw
	public float animCooldown = 1.0f;
	public float timeSinceanim = 0.0f;

	//CryOut: varaible for new equipment and attack system
	public Player player;
	public UIscript uIscript;

	// Update is called once per frame

	//	public AudioSource walk;


	void Start()
	{
		//walk= GetComponent<AudioSource>();
		player = this.GetComponent<Player>();
		uIscript = GameObject.Find("MainCanvas").GetComponent<UIscript>();
	}


	void Update()
	{
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;


		isCrouching = Physics2D.OverlapCircle(ceiling.transform.position, radiuss, ground);
		isGrounded = Physics2D.OverlapCircle(grounder.transform.position, radiuss2, ground2);

		timeSinceAction += Time.deltaTime;
		timeSincethrowAction += Time.deltaTime;
		timeSinceanim += Time.deltaTime;
		///move
		///




		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		anim.SetFloat("speed", Mathf.Abs(horizontalMove));



		///facing right?

		if (Input.GetAxisRaw("Horizontal") > 0)
		{
			facingRt = true;


		}
		else if (Input.GetAxisRaw("Horizontal") < 0)

		{
			facingRt = false;

		}



		///Jump
		/// if (Input.GetKey(KeyCode.W) && isCrouching == false && timeSinceAction >= actionCooldown)
		if (Input.GetKeyDown(KeyCode.W) && isCrouching == false && jump == false)
		{

			anim.SetBool("isJumping", true);
			///	timeSinceAction = 0;
			anim.SetBool("landing", false);
		}

		if (anim.GetBool("Jump"))
		{
			jump = true;

		}


		///Crouch
		if (Input.GetKeyDown(KeyCode.S))
		{
			crouch = true;
			anim.SetBool("isCrouching", true);



		}
		else if (Input.GetKeyUp(KeyCode.S))
		{
			crouch = false;
			anim.SetBool("isCrouching", false);
		}

		///attack
		if (Input.GetKeyDown(KeyCode.K) && isGrounded)
		{

			Debug.Log("attack");
			anim.SetBool("attack", true);

		}

		///throw
		if (Input.GetKeyDown(KeyCode.J) && isGrounded && timeSinceanim >= animCooldown)
		{
			Debug.Log("Throw");
			anim.SetTrigger("throw");
			timeSincethrowAction = 0f;
			throwbrick(0);
			timeSinceanim = 0f;
			SoundManager.PlaySound(SoundManager.Sound.playerthrow);
		}



		///throw
		if (Input.GetKeyDown(KeyCode.L) && isGrounded && timeSinceanim >= animCooldown)
		{
			Debug.Log("ThrowGlass");
			anim.SetTrigger("throw");
			timeSincethrowAction = 0f;
			throwglass(0);
			timeSinceanim = 0f;
			SoundManager.PlaySound(SoundManager.Sound.playerthrow);
		}

		//New attack system by CryOut
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if(player.EquippedItem != null && player.EquippedItem.type == "Projectile" && player.stackamount[player.EquippedSlotNumber] > 0)
			{
				if (player.EquippedItem.ID == 1)
				{
					// ID 1 : Glass Bottle
					anim.SetTrigger("throw");
					timeSincethrowAction = 0f;
					throwglass(0);
					timeSinceanim = 0f;
					SoundManager.PlaySound(SoundManager.Sound.playerthrow);
				}

				if (player.EquippedItem.ID == 2)
				{
					// ID 2 : Brick
					anim.SetTrigger("throw");
					timeSincethrowAction = 0f;
					throwbrick(0);
					timeSinceanim = 0f;
					SoundManager.PlaySound(SoundManager.Sound.playerthrow);
				}

				player.stackamount[player.EquippedSlotNumber] -= 1;
				if (player.StackChecking(player.EquippedSlotNumber) == false)
				{
					player.EquippedItem = null;
					player.EquippedSlotNumber = 100;
				}
				StartCoroutine(uIscript.inventoryReset());



				//checking stack remained
				//player.EquippedStackAmount -= 1;
				if (player.EquippedSlotNumber != 100)
				{
					uIscript.equipmentsetup(player.stackamount[player.EquippedSlotNumber]);
				}
				else
				{
					uIscript.equipmentsetup(100);
				}
			}

		}

		//
		//	if(anim.GetBool("IsRunning"))
		//{
		//		if (!footstep.isPlaying)
		//		{
		//			footstep.Play(0);
		//		}
		//}
		//	else
		//		{ walk.Stop(); }


	}


	public void OnLanding()
	{
		//	anim.SetBool("isJumping", false);
		anim.SetBool("Jump", false);
		jump = false;

		anim.SetBool("landing", true);
	}

	public void OnCrouching(bool isCrouching)
	{


	}

	public void throwglass(int value)
	{


		GameObject glassbottleclone = Instantiate(glassbottle, throwPt.transform.position, Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;

		glassbottleclone.GetComponent<Rigidbody2D>().AddForce(throwPt.up * Force, ForceMode2D.Impulse);
		glassbottleclone.GetComponent<Rigidbody2D>().AddTorque(-0.005f, ForceMode2D.Impulse);
		horizontalMove = 0f;



	}


	public void throwbrick(int value)
	{
		GameObject brickclone = Instantiate(brick, throwPt.transform.position, Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
		brickclone.GetComponent<Rigidbody2D>().AddForce(throwPt.up * Force, ForceMode2D.Impulse);
		brickclone.GetComponent<Rigidbody2D>().AddTorque(-0.01f, ForceMode2D.Impulse);
		// ... flip the player.
		horizontalMove = 0f;





	}


	// if (collision.gameObject.tag == "Enemy" )
	//	{
	//		 Destroy(brick);//}



	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);



	}


}