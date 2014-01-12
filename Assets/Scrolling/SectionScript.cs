using UnityEngine;
using System.Collections;

public class SectionScript : MonoBehaviour {

	//[HideInInspector]
	//public float _speed = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		// remove code
		//float move = _speed * Time.deltaTime;
		// move up
		//transform.Translate(Vector3.up * move, Space.World);
	}

	public void SetActive( bool bActive  ) {

		this.gameObject.SetActive( bActive );
	}

	public float sizeHeight {

		get {
			return GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.z;
		}
	}
	public float sizeWidth {
		
		get {
			return GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.x;
		}
	}

	public Vector3 movePosition( Vector3 newPosition ) {
	
		Vector3 oldPosition = transform.position;
		transform.position = newPosition;

		return oldPosition;
	}

	public Vector3 moveFollowFrame( SectionScript target, SectionTigger tiggerBox ) {

		float newX = target.transform.position.x;
		float newZ = target.transform.position.z;
		float newY = 0.0f;

		newY = target.transform.position.y - ( target.sizeHeight / 2 ) - ( this.sizeHeight / 2 );

		Vector3 newPos = new Vector3( newX, newY, newZ );
		movePosition( newPos );
		//Debug.Log( "moveFllow to Y:" + newY );

		// move trigger box
		tiggerBox.transform.position = newPos;
		//tiggerBox.ResetTigger();

		return newPos;
	}

}
