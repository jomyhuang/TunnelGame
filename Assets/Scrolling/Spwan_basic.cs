using UnityEngine;
using System.Collections;

public class Spwan_basic : MonoBehaviour {

	public int Score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if( other.tag == "Player" ) {

			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.SendMessage("playerHit");

			//Debug.Log( "player hit" );
			gameObject.SetActive( false );

			// Right way destory Destory( this.gameObject ) not Destory( this );
			Destroy( this.gameObject );
		}
	}
}
