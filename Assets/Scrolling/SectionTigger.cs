using UnityEngine;
using System.Collections;

public class SectionTigger : MonoBehaviour {

	bool isTigger = false;
	TunnelManager tunnelManager;


	// Use this for initialization
	void Start () {

		tunnelManager = TunnelManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setActive( bool bActive ) {

		gameObject.SetActive( bActive );
	}

	public void ResetTigger() {
		isTigger = false;
	}

	public void OnTriggerEnter( Collider collider ) {

		//Debug.Log("OnTriggerEnter " + collider.tag );

		if( !isTigger && collider.CompareTag("Player") ) {

			//tunnelManager.eventTiggerBox();
			tunnelManager.SendMessage ("eventTiggerBox");
			isTigger = true;
		}
	}

	public void OnTriggerExit( Collider collider ) {
		ResetTigger();
	}
}
