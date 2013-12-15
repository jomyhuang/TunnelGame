using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpwanBox : MonoBehaviour {

	public GameObject PrefabSpwan;
	public float DestroyTime = 0.0f;
	public float Probability = 0.5f;
	// Layout, num of objects

	private ArrayList SpwanObjectList = new ArrayList();

	// Use this for initialization
	void Start () {

		SpwanObjectList.Clear();
	}

	void OnEnable() {

		bool isSpwan = Random.value < Probability;
		
		if( isSpwan ) {
			GameObject spwan;
			//spwan = Instantiate( coin, transform.position, Quaternion.identity ) as GameObject;
			spwan = Instantiate( PrefabSpwan ) as GameObject;

			spwan.transform.parent = this.transform;
			spwan.transform.localPosition = new Vector3(0,0,0);

			//Debug.Log ( "spwan" + spwan.transform.position.y );

			if( DestroyTime > 0.0f ) {
				Destroy( spwan, DestroyTime );
			}
			else {
				SpwanObjectList.Add ( spwan );
			}
		}
	}

	void OnDisable() {

		/*
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Spwan");
		Debug.Log ( "destroy log gos " + gos.Length );
		*/

	
		// TODO: if spwan object alreay destroy?
		foreach( GameObject obj in SpwanObjectList ) {
			//if( obj )
				Destroy( obj );
		}
		SpwanObjectList.Clear();
	}

	// Update is called once per frame
	void Update () {


	}
}
