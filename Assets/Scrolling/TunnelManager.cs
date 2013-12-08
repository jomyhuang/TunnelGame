using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TunnelManager : MonoBehaviour {

	public float _speed = 0.0f;
	// scrolling direction -1:down 1:up
	//public int _Direction = -1; 
	public List<SectionScript> _sections = new List<SectionScript>();
	public SectionTigger _tiggerBox;

	private int _currentList = 0;
	private SectionScript _currentSection = null;
	private float _screenUnitHeight = 0.0f;

	// Treat this class as a singleton.  This will hold the instance of the class.
	private static TunnelManager instance;

	private int _countSection = 0;
	public int countSection
	{
		get { return _countSection; }
	}

	private float _startTime;
	public float startTime
	{
		get { return _startTime; }
	}
	public float countTime
	{
		get { return Time.time - _startTime; }
	}
	
	public static TunnelManager Instance
	{
		get
		{
			// This should NEVER happen, so we want to know about it if it does 
			if(instance == null)
			{
				Debug.LogError("TunnelManager instance does not exist");	
			}
			return instance;	
		}
	}

	void Awake()
	{
		instance = this; 
	}

	IEnumerator Start()
	{
		calcScreenUnitHeight();

		yield return 0;
		initSections();
	}

	void OnGUI() {

		/*
		if( _currentSection ) {
			
			GUILayout.Label( "current: " + _currentList + " pos:" + _currentSection.transform.position );
			//GUILayout.Label( "_disapperPosY :" + _disapperPosY );
			//if( _currentFrame.transform.position.y < _disapperPosY ) {
			//	GUILayout.Label( "Disapper" );
			//}
			GUILayout.Label( "screen height:" + _screenUnitHeight + " speed:" + _speed );

		}
		*/
	}
	
	void Update()
	{
	
	}

	public void setSpeed( float newSpeed ) {

		//yield return 0;

		_speed = newSpeed;
		foreach( SectionScript frame in _sections ) {
			frame._speed = _speed;
		}
		_tiggerBox._speed = _speed;
	}

	public float getSpeed() {
		return _speed;
	}

	public void eventTiggerBox() {

		moveToNextSection();
	}

	public void eventOverWorldSize( Rigidbody player ) {
		
		//Debug.Log ("event over world size");

		//player.isKinematic = true;
		float playerNewPosX = player.transform.position.x;
		float playerNewPosY = 0;
		float playerNewPosZ = player.transform.position.z;

		/// calc current setion new position
		float oldPosY = player.transform.position.y;
		float deltaMovePosY = _currentSection.transform.position.y + Mathf.Abs( playerNewPosY -  oldPosY );
		// -300 + ( 0 - -520)
		_currentSection.movePosition( new Vector3( 0, deltaMovePosY, 2 ) );

		SectionScript nextSection = getSection( nextList() );
		nextSection.moveFollowFrame( _currentSection, _tiggerBox );

		// player move to new position
		player.transform.position = new Vector3( playerNewPosX, playerNewPosY, playerNewPosZ );

		Debug.Log ("event over world size new pos y:" + player.transform.position.y + " new section " + deltaMovePosY );
	}	

	void initSections() {


		// init all frames
		foreach( SectionScript frame in _sections ) {

			setSectionActive( frame, false );
		}
		// turn on scrolling mode
		//setSpeed( this._speed );
		//_tiggerBox.setActive( false );

		_currentList = 0;
		_currentSection = getSection( _currentList );

		// todo: determine init pos;
		float initPosX = 0;
		float initPosY = 0;
		float initPosZ = 2;

		_currentSection.movePosition( new Vector3( initPosX, initPosY, initPosZ ) );

		SectionScript nextSection = getSection( nextList() );
		nextSection.moveFollowFrame( _currentSection, _tiggerBox );

		// active
		setSectionActive( _currentSection, true );
		setSectionActive( nextSection, true );
		//_tiggerBox.setActive( true );


		//Debug.Log ( "Manager initScrolling OK" );

		_countSection = 0;
		_startTime = Time.time;

		// Time Pause
		//Time.timeScale = 0;
	}

	void moveToNextSection() {

		// de-active disapper frame
		setSectionActive( _currentSection, false );

		// link next frame
		_currentList = nextList();
		_currentSection = getSection( _currentList );

		SectionScript nextSection = getSection( nextList() );
		nextSection.moveFollowFrame( _currentSection, _tiggerBox );

		// active next frame
		setSectionActive( nextSection, true );

		// section counter by 1
		_countSection++;
	}
	
	void calcScreenUnitHeight() {

		Vector3 cameraTrans = new Vector3( Screen.width, Screen.height, 0 );
		Vector3 unitCameraTrans = Camera.main.ScreenToWorldPoint( cameraTrans );
		
		_screenUnitHeight = unitCameraTrans.y;
	}

	SectionScript getSection( int count ) {

		return _sections[ count ];
	}

	int nextList() {

		int next = _currentList + 1;
		if( next > _sections.Count -1 ) {
			next = 0;
		}

		return next;
	}

	void setSectionActive( SectionScript frame, bool isActive ) {

		frame.SetActive( isActive );
	}
}