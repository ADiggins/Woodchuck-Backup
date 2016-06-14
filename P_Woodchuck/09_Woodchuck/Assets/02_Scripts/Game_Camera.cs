using UnityEngine;
using System.Collections;

public class Game_Camera : MonoBehaviour {
	public GameObject m_player;
	// Use this for initialization
	void Start () {
	
	}
	public float f = 0;
	// Update is called once per frame
	void Update () {
		//f += Time.deltaTime;
		//Debug.Log (!Input.GetKeyDown (KeyCode.A));

		//Debug.Log (Input.GetKeyUp (KeyCode.D));

		//Debug.Log (Input.GetKeyUp (KeyCode.A) && Input.GetKeyUp (KeyCode.D));


		f += Input.GetAxis ("Mouse X") * Time.deltaTime;
		this.transform.position = m_player.transform.position + new Vector3(Mathf.Sin(f) * 3, 2 , Mathf.Cos(f) * 3 );		
		//transform.RotateAround(m_player.transform.position, Vector3.up, Input.GetAxis("Mouse X")* Time.deltaTime * 360.0f);
		//transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")* Time.deltaTime * 360.0f, 0) );

		//f = transform.localRotation.eulerAngles.y;
		//f = f * Mathf.Deg2Rad;
		//Debug.Log(transform.localRotation.eulerAngles.y);
		//
		//this.transform.position = m_player.transform.position  + new Vector3 (Mathf.Sin(f) * 5.0f, 3.3f , Mathf.Cos(f) * 5.0f);
		transform.LookAt (m_player.transform.position + new Vector3(0,1,0));
	}
}
