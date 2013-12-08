using UnityEngine;

using System.Collections;



public class MazePlayerMotor : MonoBehaviour {
	
	
	
	public Vector3 targetDirection;
	
	
	
	// Use this for initialization
	
	void Start () {
		
	}

	// Update is called once per frame
	
	void Update () {
		
		targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				
		
		if (Input.GetButton("Fire1")){
			
			animation.Play("Attack");
			

		}else if(Input.GetKey(KeyCode.E)){
			
			animation.Play("pickup");

		}else if (targetDirection.magnitude > 0.1) {
			
			animation.Play("Run");

			transform.rotation = Quaternion.LookRotation(targetDirection);
			
			CharacterController conroller = GetComponent<CharacterController>();
			
			conroller.Move(transform.forward * Time.deltaTime * 3f);
			
		}else{
			
			animation.Play("Idle");
			
		}
		
	}
	
}