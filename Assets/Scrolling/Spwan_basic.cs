using UnityEngine;
using System.Collections;

public class Spwan_basic : MonoBehaviour {

	public int Score = 0;
	public GameObject prefabFXApper;
	public GameObject prefabFXTigger;

	// Use this for initialization
	void Start () {
	
		if( prefabFXApper ) {

			spawnParticle( prefabFXApper, this.gameObject );
		}
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate( new Vector3( 15, 15, 15 ) * Time.deltaTime );
	
	}

	void OnTriggerEnter(Collider other) {

		if( other.tag == "Player" ) {

			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.SendMessage("playerHit");

			//Debug.Log( "player hit" );
			gameObject.SetActive( false );

			// Right way destory Destory( this.gameObject ) not Destory( this );
			Destroy( this.gameObject );

			if( prefabFXTigger ) {

				spawnParticle( prefabFXTigger, player );
				//Instantiate( PrefabFXTigger, player.transform.position, Quaternion.identity );
			}
		}
	}

	private GameObject spawnParticle( GameObject perfabFX, GameObject attach )
	{
		GameObject particles = (GameObject) Instantiate( perfabFX );
		
		#if UNITY_3_5
		particles.SetActiveRecursively(true);
		#else
		particles.SetActive(true);
		for(int i = 0; i < particles.transform.childCount; i++)
			particles.transform.GetChild(i).gameObject.SetActive(true);
		#endif

		/*
		float Y = 0.0f;
		foreach(KeyValuePair<string,float> kvp in ParticlesYOffsetD)
		{
			if(particles.name.StartsWith(kvp.Key))
			{
				Y = kvp.Value;
				break;
			}
		}
		*/

		particles.transform.parent = attach.transform;
		particles.transform.localPosition = new Vector3(0,0,0);
		
		return particles;
	}

}
