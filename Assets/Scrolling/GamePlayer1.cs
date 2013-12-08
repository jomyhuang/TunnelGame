using UnityEngine;
using System.Collections;

public class GamePlayer1 : MonoBehaviour {

	public ScrollingManager manager;

	// Use this for initialization
	//IEnumerator Start () {
	void Start () {

		//rigidbody.useGravity = false;
		//yield return new WaitForSeconds(5);
		//rigidbody.useGravity = true;
	}
	
	// Update is called once per frame
	void Update () {

		if( manager ) {
			if( Input.GetKeyDown ( "right" ) ) {
				manager.setSpeed( manager._Speed + 1 );
			}
		}


	}
}
