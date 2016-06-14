using UnityEngine;
using System.Collections;

public class Game_PlayerController : MonoBehaviour {
	private Vector3 currentLookPos; 
	public Transform lookTarget { get; set; } // The point where the character will be looking at
	private float forwardAmount;
	private Vector3 moveInput;
	private float turnAmount;
	public Vector3 move;
	Vector3 lookPos;
	private Transform cam;
	public bool lookInCameraDirection = true;
	// Use this for initialization
	public Animator m_anim;
	public Vector3 vv;

	int m_attack1;
	int m_attack2;
	int m_attack3;
	int m_roll;
	int m_running;

	void Start () {
		cam = Camera.main.transform;
		m_anim = GetComponent<Animator> ();


		m_attack1 = Animator.StringToHash("Attack1");
		m_attack2 = Animator.StringToHash("Attack2");
		m_attack3 = Animator.StringToHash("Attack3");
		m_roll = Animator.StringToHash("Roll");
		m_running = Animator.StringToHash("Running");
	}
	int m_at =0 ;
	// Update is called once per frame
	void Update () {
		

		//transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")* Time.deltaTime * 360.0f, 0) );
		// read inputs
	

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		//Debug.Log (Input.GetKeyUp (KeyCode.A) && Input.GetKeyUp (KeyCode.D));
		if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {
			this.transform.forward = cam.transform.forward;
			this.transform.right = cam.transform.right;
			m_anim.SetBool (m_running, false);

		} else {
			//this.GetComponent<Rigidbody>().AddForce(this.transform.forward * Time.deltaTime * 500.0f);
			this.transform.position += this.transform.forward * Time.deltaTime * 5.0f;
			m_anim.SetBool (m_running, true);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			m_anim.SetTrigger (m_roll);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			m_at +=1;
			if(m_at == 1)
			{
				m_anim.SetTrigger (m_attack1);
			}
			else if(m_at ==2)
			{
				m_anim.SetTrigger (m_attack2);
			}
			else if(m_at == 3)
			{
				m_anim.SetTrigger (m_attack3);
			}
			if(m_at > 3)
			{
				m_at = 0;
			}
		}

		// calculate move direction to pass to character
		if (cam != null)
		{
			// calculate camera relative direction to move:
			camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
			move = v*camForward + h*cam.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			move = v*Vector3.forward + h*Vector3.right;
		}

		if (move.magnitude > 1) move.Normalize();

		// calculate the head look target position
		lookPos = lookInCameraDirection && cam != null
			? transform.position + cam.forward*100
			: transform.position + transform.forward*100;
		lookPos = currentLookPos = transform.position + transform.forward * 100;
		// pass all parameters to the character control script

	
		if (lookTarget != null)
		{
			currentLookPos = lookTarget.position;
		}
		this.moveInput = move;
		ConvertMoveInput ();
		TurnTowardsCameraForward ();
		ApplyExtraTurnRotation ();
	}

	private void ConvertMoveInput()
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction. 
		Vector3 localMove = transform.InverseTransformDirection(moveInput);
		turnAmount = Mathf.Atan2(localMove.x, localMove.z);
		forwardAmount = localMove.z;
	}
	private void TurnTowardsCameraForward()
	{
		// automatically turn to face camera direction,
		// when not moving, and beyond the specified angle threshold
		if (Mathf.Abs(forwardAmount) < .01f)
		{
			Vector3 lookDelta = transform.InverseTransformDirection(currentLookPos - transform.position);
			float lookAngle = Mathf.Atan2(lookDelta.x, lookDelta.z)*Mathf.Rad2Deg;

			// are we beyond the threshold of where need to turn to face the camera?
			if (Mathf.Abs(lookAngle) > 100.0f)
			{
				turnAmount += lookAngle* 2.0f *.001f;
			}
		}
	}

	private Vector3	camForward;
	void FixedUpdate()
	{

	}






	private void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(180.0f, 360.0f,forwardAmount);

		transform.Rotate(0, turnAmount*turnSpeed*Time.deltaTime, 0);
	}

}
