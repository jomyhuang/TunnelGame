using UnityEngine;
using System.Collections;

public class SectionTigger : MonoBehaviour {

	[HideInInspector]
	public float _speed = 0.0f;

	bool isTigger = false;
	TunnelManager tunnelManager;


	// Use this for initialization
	void Start () {

		tunnelManager = TunnelManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
		float move = _speed * Time.deltaTime;
		// move up
		transform.Translate(Vector3.up * move, Space.World);
	}

	public void setActive( bool bActive ) {

		gameObject.SetActive( bActive );
	}

	public void ResetTigger() {
		isTigger = false;
	}

	public void OnTriggerEnter( Collider collider ) {

		/*
		if ((collider.transform.tag == "Player") && (rocks.GetTrigger() ==
		                                             false)) {
			rocks.EnabledRigidbody();
			rocks.SetTrigger(true);
		} }
		*/

		//Debug.Log("OnTriggerEnter " + collider.tag );

		if( !isTigger && collider.CompareTag("Player") ) {

			//Debug.Log("OnTriggerEvent");
			tunnelManager.eventTiggerBox();
			isTigger = true;
		}
	}

	public void OnTriggerExit( Collider collider ) {
		ResetTigger();
	}
}
