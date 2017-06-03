using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYCON : MonoBehaviour {

    public Animator myAnimationController;

	// Use this for initialization
	void Start () {
        myAnimationController.Play("My Animation");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
