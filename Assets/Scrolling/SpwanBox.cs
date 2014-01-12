using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpwanBox : MonoBehaviour {

	public GameObject PrefabSpwan;
	public float DestroyTime = 0.0f;
	public float Probability = 0.5f;
	// Layout, num of objects

	//private ArrayList SpwanObjectList = new ArrayList();

	// Use this for initialization
	void Start () {

		//SpwanObjectList.Clear();
	}

	void OnEnable() {

		bool isSpwan = Random.value < Probability;
		
		if( isSpwan ) {

			GameObject spwan;
			//spwan = Instantiate( coin, transform.position, Quaternion.identity ) as GameObject;
			spwan = Instantiate( PrefabSpwan ) as GameObject;

			float offset = Random.Range ( -3, 3 ); 

			spwan.transform.parent = this.transform;
			spwan.transform.localPosition = new Vector3( 0 + offset, 0, 0 );

			//Debug.Log ( "spwan" + spwan.transform.position.y );

			if( DestroyTime > 0.0f ) {
				Destroy( spwan, DestroyTime );
			}
			/*
			else {
				SpwanObjectList.Add ( spwan );
			}
			*/
		}
	}

	void OnDisable() {

		/*
		foreach( GameObject obj in SpwanObjectList ) {
			//if( obj )
				Destroy( obj );
		}
		SpwanObjectList.Clear();
		*/

		// delete all child objects 
		for( int i = 0; i < this.transform.childCount; i++ )
		{
			GameObject go = this.transform.GetChild(i).gameObject;
			Destroy( go );
		}
	}

	// Update is called once per frame
	void Update () {


	}
}
