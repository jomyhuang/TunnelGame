using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScrollingManager : MonoBehaviour {

	public float _Speed = 0.0f;
	// scrolling direction -1:down 1:up
	public int _Direction = -1; 
	public List<ScrollingScript> scrollings = new List<ScrollingScript>();
	private int _currentScrolling = 0;
	private ScrollingScript _currentFrame = null;
	private float _disapperPosY = 0.0f;
	private float _screenUnitHeight = 0.0f;

	// Treat this class as a singleton.  This will hold the instance of the class.
	private static ScrollingManager instance;
	
	public static ScrollingManager Instance
	{
		get
		{
			// This should NEVER happen, so we want to know about it if it does 
			if(instance == null)
			{
				Debug.LogError("ScrollingScript instance does not exist");	
			}
			return instance;	
		}
	}

	void Awake()
	{
		instance = this; 
	}

	/*
	public void RegisterScrollling(ScrollingScript who)
	{
		Debug.Log ("RegisterScrollling:Add scrolling");
		//scrollings.Add(who);
	}
	*/

	IEnumerator Start()
	{
		//float xSize=GetComponent<MeshFilter>().mesh.bounds.size.x*transform.localScale.x;
		//float ySize=GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.z;

		//print ( xSize + " ysize:" + ySize );
		calcScreenUnitHeight();

		yield return 0;
		initScrolling();
	}

	void OnGUI() {

		if( _currentFrame ) {
			
			GUILayout.Label( "current: " + _currentScrolling + " pos:" + _currentFrame.transform.position + " sizeHeight:" + _currentFrame.sizeHeight );
			GUILayout.Label( "_disapperPosY :" + _disapperPosY );
			//if( _currentFrame.transform.position.y < _disapperPosY ) {
			//	GUILayout.Label( "Disapper" );
			//}
			GUILayout.Label( "screen height:" + _screenUnitHeight + " speed:" + _Speed );

		}

	}
	
	void Update()
	{
		/*
		float Move = _Speed * Time.deltaTime;
		transform.Translate(Vector3.down * Move,Space.World);
		*/

		/*
		if(transform.position.y<-20)
		{
			transform.position = new Vector3(transform.position.x,
			                                 20,transform.position.z);
		}
		*/

		if( _currentFrame ) {

			bool bDisapper = false;
			if( _Direction > 0 ) {
				// move up
				if( _currentFrame.transform.position.y > _disapperPosY ) {
					bDisapper = true;
				}
			}
			else {
				// move down
				if( _currentFrame.transform.position.y < _disapperPosY ) {
					bDisapper = true;
				}
			}

			if( bDisapper ) {

				// de-active disapper frame
				_currentFrame.SetActive( false );

				// link next frame
				_currentScrolling = nextScrolling();
				_currentFrame = getFrame( _currentScrolling );
				_disapperPosY = _currentFrame.calcDisapperPosY( _screenUnitHeight );
				
				ScrollingScript nextFrame = getFrame( nextScrolling() );
				nextFrame.moveFollowFrame( _currentFrame );

				// active next frame
				nextFrame.SetActive( true );
			}
		}
	}

	public void setSpeed( float newSpeed ) {

		//yield return 0;

		_Speed = newSpeed;
		foreach( ScrollingScript frame in scrollings ) {
			frame._Speed = _Speed;
		}
	}

	public float getSpeed() {
		return _Speed;
	}

	void initScrolling() {


		// init all frames
		foreach( ScrollingScript frame in scrollings ) {

			//frame.gameObject.SetActive( false );
			//frame._Speed = this._Speed;
			frame._Speed = _Speed;
			frame._Direction = _Direction;
			frame.SetActive( false );
		}

		_currentScrolling = 0;
		_currentFrame = getFrame( _currentScrolling );
		// todo: determine init pos;
		float initPosX = 0;
		float initPosZ = 2;
		float initPosY = 0;

		if( _Direction > 0 ) {
			// move up
			initPosY = 0;
		}
		else {
			// move down
			initPosY = _currentFrame.sizeHeight / 2 - _screenUnitHeight;
		}

		_currentFrame.movePosition( new Vector3( initPosX, initPosY, initPosZ ) );
		_disapperPosY = _currentFrame.calcDisapperPosY( _screenUnitHeight );

		ScrollingScript nextFrame = getFrame( nextScrolling() );
		nextFrame.moveFollowFrame( _currentFrame );

		// active
		_currentFrame.SetActive( true );
		nextFrame.SetActive( true );

		//Transform tScrolling = scrolling.gameObject.transform;
		//tScrolling.position = new Vector3(0,0,2);

		Debug.Log ( "Manager initScrolling" );
	}

	void calcScreenUnitHeight() {

		//Vector3 cameraTrans = new Vector3( Camera.main.pixelWidth, Camera.main.pixelHeight, 0 );
		Vector3 cameraTrans = new Vector3( Screen.width, Screen.height, 0 );
		Vector3 unitCameraTrans = Camera.main.ScreenToWorldPoint( cameraTrans );
		
		_screenUnitHeight = unitCameraTrans.y;
	}

	ScrollingScript getFrame( int count ) {

		return scrollings[ count ];
	}

	int nextScrolling() {

		int next = _currentScrolling + 1;
		if( next > scrollings.Count -1 ) {
			next = 0;
		}

		return next;
	}

}