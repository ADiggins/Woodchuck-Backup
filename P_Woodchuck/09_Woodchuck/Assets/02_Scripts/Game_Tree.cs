using UnityEngine;
using System.Collections;

public class Game_Tree : MonoBehaviour {

    Animator m_anim;
	// Use this for initialization
	void Start () {
	    m_anim = this.GetComponent<Animator>();
		

    }
	
	// Update is called once per frame
	void Update () {

	}


	void  OnTriggerStay(Collider other)
    {
		if (other.gameObject.tag == "Player")
        {
			AnimatorStateInfo a = other.gameObject.GetComponent<Game_PlayerController>().m_anim.GetCurrentAnimatorStateInfo(0); 
			Debug.Log(a.IsName("anim_Chuck_attack1"));
			if(a.IsName("anim_Chuck_attack1") || a.IsName("anim_Chuck_attack2") || a.IsName("anim_Chuck_attack3"))
			{

				//m_anim.SetTrigger("Trigger_Fall");
			}
        }
    }
}
