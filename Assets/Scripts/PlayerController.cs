using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class PlayerController : MonoBehaviour {

	/// <summary>
	/// The movement speed of the player on the ground.
	/// </summary>
	public float movementSpeed;


	/// <summary>
	/// The movement speed mod.
	/// Multiplied with movement speed before movement speed is aplied.
	/// </summary>
	private float movementSpeedMod = 1.0f;

	/// <summary>
	/// The flight speed of the player.
	/// </summary>
	public float flightSpeed;

	/// <summary>
	/// The swim speed of the player.
	/// </summary>
	public float swimSpeed;

	/// <summary>
	/// The look sensitivity.
	/// </summary>
	public float lookSensitivity;

	/// <summary>
	/// Lock the cursor by default?
	/// </summary>
	public bool lockCursor;

	/// <summary>
	/// The maximum height of the player.
	/// </summary>
	public float maximumHeight;

	/// <summary>
	/// The maximum ascent speed of the player.
	/// </summary>
	public float maximumAscentSpeed;

	/// <summary>
	/// The normal ascent speed.
	/// </summary>
	public float ascentSpeed;

	/// <summary>
	/// The duck animator.
	/// </summary>
	public Animator duckAnim;

	/// <summary>
	/// The duck wings object.
	/// </summary>
	public GameObject duckWings;

	public GameObject invincibleKillEffect;

	public GameObject jumpWeapon;


	/// <summary>
	/// Height at which the player is considered to be flying.
	/// </summary>
	public float flightHeight = 0.25f;


    public float powerupTime = 10.0f;
    float powerupTimeElapsed = 0.0f;
    

	//current height of the player
	float currentHeight;

	//players rigidbody. autoassigned
	Rigidbody r;

	PlayerStates p;

	// Use this for initialization
	void Start () {
		//Assign the players rigidbody.
		r = this.GetComponent<Rigidbody> ();
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		//get easy, readable reference to playerstates.
		p = PlayerStates.instance;
        p.playerController = this;
	}
	
	// Update is called once per physics update
	void FixedUpdate () {
		//pos is altered throghout this call then applied at the end.
		Vector3 pos = transform.position;


		//quadruple duck speed if its invincible.
		movementSpeedMod = invincible ()? 4.0f : 1.0f;

		//If shroomed then activate the jump weapon.
		jumpWeapon.SetActive (shroomed ());

		//Handles input from the player
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
			if (p.currentState == PlayerStates.State.Walking) {
				pos += transform.forward * Input.GetAxis ("Vertical") * movementSpeed * movementSpeedMod * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * movementSpeed * movementSpeedMod * Time.deltaTime;
			} else if(p.currentState == PlayerStates.State.Flying) {
				pos += transform.forward * Input.GetAxis ("Vertical") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed) * movementSpeedMod * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed) * movementSpeedMod * Time.deltaTime;
			} else {
				pos += transform.forward * Input.GetAxis ("Vertical") * swimSpeed * movementSpeedMod * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * swimSpeed * movementSpeedMod * Time.deltaTime;
			}
		}
		//looking around
		if (Input.GetAxis ("Mouse X") != 0) {
			transform.RotateAround(transform.position, Vector3.up ,Input.GetAxis("Mouse X") * lookSensitivity);
		}


		if (Input.GetButton ("Jump")) {

			//Jumping because mushroom mode.
			if (shroomed ()) {
				if (distanceToGround() <= 1.0f) {
                    jump(10);
				}
			}
			//flying up
			else if (CanFly() && p.currentState != PlayerStates.State.Falling) {
				startFlying ();
				pos.y += ascentSpeed * Time.deltaTime;
			}
		}


		//flying down
		if (Input.GetButton ("Decend") && p.currentState == PlayerStates.State.Flying) {
			pos.y -= ascentSpeed * Time.deltaTime;
		}

		//landing
		if (distanceToGround () <= flightHeight && p.currentState == PlayerStates.State.Flying) {
			startWalking();
		}

		if (p.currentState == PlayerStates.State.Flying) {
			flying ();
		}

		if (p.currentState == PlayerStates.State.Falling) {
			flying ();
			pos.y -= 3*Time.deltaTime;
		}

		if (p.currentState == PlayerStates.State.Walking) {
			walking ();
		}

		if (p.currentState == PlayerStates.State.Swimming) {
			swimming ();
		}

		//apply transformation
		r.MovePosition (pos);


        powerupTimeElapsed += Time.deltaTime;

        if (p.currentPowerupState != PlayerStates.PowerUpState.None && powerupTimeElapsed >= powerupTime)
        {
            p.cancelPowerups();
        }
	}

	public bool CanFly()
	{
		return transform.position.y <= maximumHeight && p.energy >= 0;
	}

	/// <summary>
	/// Easy access to know if we have the invincible powerup.
	/// </summary>
	public bool invincible()
	{
		return p.currentPowerupState == PlayerStates.PowerUpState.Invincible;
	}

	/// <summary>
	/// Easy access to know if we have the shroomed powerup.
	/// </summary>
	public bool shroomed()
	{
		return p.currentPowerupState == PlayerStates.PowerUpState.Shroomed;
	}

	/// <summary>
	/// Distance to ground from the player.
	/// </summary>
	/// <returns>The to ground.</returns>
	public float distanceToGround () {		
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
			return hit.distance;
		} else {
			return -1;
		}
	}

	/// <summary>
	/// Starts flying.
	/// </summary>
	public void startFlying () {
		r.velocity = new Vector3(0,0,0);
		r.useGravity = false;
		duckWings.SetActive(true);;
		duckAnim.Play("Flying");
		p.currentState = PlayerStates.State.Flying;
	}

	/// <summary>
	/// Called each frame the player is flying
	/// </summary>
	public void flying () {
		if(distanceToGround() <= flightHeight) {
			startWalking();
		}
		if (p.energy == 0) {
			startFalling();
		}
	}

	/// <summary>
	/// Starts falling.
	/// </summary>
	public void startFalling () {
		//duckWings.SetActive (false);
		//duckAnim.Play ("Walking");
		p.setState(PlayerStates.State.Falling);
	}

	/// <summary>
	/// Starts walking
	/// </summary>
	public void startWalking () {
		duckWings.SetActive(false);
		r.useGravity = true;
		duckAnim.Play("Walking");
		p.currentState = PlayerStates.State.Walking;
	}

	/// <summary>
	/// Called each frame we're walking
	/// </summary>
	public void walking () {
		if (transform.position.y <= 0) {
			startSwimming();
		}

	}

	/// <summary>
	/// Starts swimming
	/// </summary>
	public void startSwimming () {
		p.currentState = PlayerStates.State.Swimming;
		duckWings.SetActive (false);
		duckAnim.Play ("Walking");
	}

	/// <summary>
	/// Called each frame we're swimming.
	/// </summary>
	public void swimming () {
		if (transform.position.y >= 0 && transform.position.y <= flightHeight) {
			startWalking();
		}
	}




	//
	// assessment 3 addition for powerups below to end of script.
	//




	void OnCollisionEnter (Collision c) {
		if (c.transform.tag == "Enemy") {
			//If we bump in to an enemy while we are invincible then kill them.
			if (invincible ()) {
				if(c.transform.name != "GiantGoose") {
					Enemy e = c.gameObject.GetComponent<Enemy> ();
					e.decreaseHealth (e.health);
					Instantiate (invincibleKillEffect, c.transform.position, Quaternion.identity);
				}
			}
		}
	}


    /// <summary>
    ///Called by player states when the current powerup state changes.
    /// </summary>
	public void onPowerupStateChanged(PlayerStates.PowerUpState value)
    {
        if (value == PlayerStates.PowerUpState.Shroomed)
        {
            if (p.currentState == PlayerStates.State.Flying)
            {
                startWalking();
            }

            grow();

        }


        if (value == PlayerStates.PowerUpState.Invincible)
        {
            shrink();
            ///TODO Make player colourful.
        }


        if (value == PlayerStates.PowerUpState.None)
        {
            shrink();
        }
        else
        {
            //We have got a powerup so start the timer to disable it.
            powerupTimeElapsed = 0.0f;
        }
    }


    /// <summary>
    /// Jump up (so we dont fall through the ground) and grow.
    /// </summary>
    public void grow() {
        jump(12);
        transform.DOScale(0.3f, 0.5f)
                .SetEase(Ease.InBounce);
    }

    /// <summary>
    /// Scale back to normal size.
    /// </summary>
    public void shrink(){
        transform.DOScale(0.1f, 0.5f)
            .SetEase(Ease.InBounce);
    }


    /// <summary>
    /// Jump up by the amount.
    /// </summary>
    /// <param name="amount"></param>
    public void jump(float amount)
    {
        r.velocity = new Vector3(0, amount, 0);
    }
    }