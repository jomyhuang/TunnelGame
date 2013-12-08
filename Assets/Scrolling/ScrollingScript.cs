using UnityEngine;
using System.Collections;

public class ScrollingScript : MonoBehaviour {

	public float _Speed = 0.0f;
	public int _Direction = -1; 
	public float testSpeed = 10.0f;
	public float _testSpeed = 10.0f;
	//public bool _isRendering = false;
	//private float lastTime = 0.0f;
	//private float curtTime = 0.0f;
	//private bool flagActive = false;

	// Use this for initialization
	void Start () {


		// add to Scrolling container
		//ScrollingManager.Instance.RegisterScrollling(this);
	}
	
	// Update is called once per frame
	void Update () {

		float Move = _Speed * Time.deltaTime;
		if( _Direction > 0 ) {
			// move up
			transform.Translate(Vector3.up * Move, Space.World);
		} 
		else {
			// move down 
			transform.Translate(Vector3.down * Move, Space.World);
		}

		// check object is Rending?
		//_isRendering = curtTime != lastTime ? true : false;

		/*
		if( lastTime != curtTime )
			_isRendering = true;
		else 
			_isRendering = false;

		lastTime = curtTime;
		*/
		/*
		Vector3 screenPos = Camera.main.WorldToScreenPoint( transform.position );
		if( screenPos.y > 0 ) {
			_isRendering = true;
		} else {
			_isRendering = false;
		}

		if( _isRendering ) {
			if( !flagActive ) { 
				Debug.Log ("object active & visible");
				flagActive = true;
			}
		}
		if( flagActive && !_isRendering ) {
			Debug.Log( "object is gone" );
			flagActive = false;
		}
		*/
	}

	public void SetActive( bool bActive  ) {

		/*
		if( bActive ) {

		}
		else {

		}
		*/
		this.gameObject.SetActive( bActive );
	}

	public float sizeHeight {

		//float xSize=GetComponent<MeshFilter>().mesh.bounds.size.x*transform.localScale.x;
		//float ySize=GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.z;
		get {
			return GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.z;
		}
	}
	public float sizeWidth {
		
		//float xSize=GetComponent<MeshFilter>().mesh.bounds.size.x*transform.localScale.x;
		//float ySize=GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.z;

		get {
			return GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.x;
		}
	}


	public Vector3 movePosition( Vector3 newPosition ) {
	
		Vector3 oldPosition = transform.position;
		transform.position = newPosition;

		return oldPosition;
	}
	public Vector3 moveFollowFrame( ScrollingScript target ) {

		float newX = target.transform.position.x;
		float newZ = target.transform.position.z;
		float newY = 0.0f;


		if( _Direction > 0 ) {
			// move up, match bottom
			newY = target.transform.position.y - ( target.sizeHeight / 2 ) - ( this.sizeHeight / 2 );
		} 
		else {
			// move down, match top 
			newY = target.transform.position.y + ( target.sizeHeight / 2 ) + ( this.sizeHeight / 2 );
		}

		Vector3 newPos = new Vector3( newX, newY, newZ );
		movePosition( newPos );
		Debug.Log( "moveFllow to Y:" + newY );

		return newPos;
	}


	public float calcDisapperPosY( float screenUnitHeight ) {

		/*
		float disY = ( sizeHeight() / 2 ) * -1;
		Vector3 disPos = new Vector3( transform.position.x, disY, transform.position.z );
		Vector3 screenPos = Camera.main.WorldToScreenPoint( disPos );
		float screenDisY = screenPos.y - Camera.main.pixelHeight / 2;
		*/

		// disapper pos = ( sizeHeight / 2 + screenHeight / 2 )  * -1
		/*
		float disY = ( this.sizeHeight / 2 ) * -1;
		Vector3 disPos = new Vector3( transform.position.x, disY, transform.position.z );
		Vector3 screenPos = Camera.main.WorldToScreenPoint( disPos );
		float screenDisY = screenPos.y - Screen.height / 2;

		Vector3 correctDisPos = new Vector3( transform.position.x, screenDisY, transform.position.z );
		Vector3 unitDis = Camera.main.ScreenToWorldPoint( correctDisPos );
		float unitDisY = unitDis.y;
		*/

		//float disY = ( this.sizeHeight / 2 ) * -1;
		//Vector3 disPos = new Vector3( transform.position.x, disY, transform.position.z );
		//Vector3 screenPos = Camera.main.WorldToScreenPoint( disPos );
		//float screenDisY = screenPos.y - Screen.height / 2;
		
		//Vector3 correctDisPos = new Vector3( transform.position.x, screenDisY, transform.position.z );
		//Vector3 unitDis = Camera.main.ScreenToWorldPoint( correctDisPos );
		float unitDisY = 0.0f;

		if( _Direction > 0 ) {
			// move up
			unitDisY = ( this.sizeHeight / 2 + screenUnitHeight );
		}
		else {
			// move down
			unitDisY = ( this.sizeHeight / 2 + screenUnitHeight ) * -1;
		}

		Debug.Log ( "calcDisapperPosY unitDisY:" + unitDisY );

		return unitDisY;
	}

	/*
	void OnWillRenderObject()
	{
		curtTime=Time.time;
	}
	*/
}
