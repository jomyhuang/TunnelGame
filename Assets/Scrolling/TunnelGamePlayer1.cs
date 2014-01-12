using UnityEngine;
using System.Collections;

public class TunnelGamePlayer1 : MonoBehaviour {


	public float _UpSpeed = 0.0f;
	public float _maxFalldownSpeed = 25.0f;

	public float _moveForce =  50.0f;    //365f;	
	public float _maxMoveSpeed = 3.0f;
	private TunnelManager _tunnelManager;

	private float _playerGravity =  9.81f;
	private float _playerVelocity = 0.0f;

	private float _playerMoveSpeed = 5.0f;
	private float _playerMoveX = 0.0f;
	private float _playerMoveY = 0.0f;

	private float _playerInDirection = 0.0f;
	private float _lastMoveTime;

	enum state { Start, Falldown, Die }; 
	private state _playerState = state.Start;


	// Use this for initialization
	//IEnumerator Start () {
	void Start() {
		_tunnelManager = TunnelManager.Instance;

		//rigidbody.isKinematic = true;
		//yield return new WaitForSeconds(2);
		//rigidbody.isKinematic = false;
	}

	void OnGUI() {
		
		GUILayout.Label( "player pos:" + transform.position + "  velocity:" + rigidbody.velocity );
		GUILayout.Label( "up speed:" + _UpSpeed + " playerGravity:" + _playerGravity + " playerVelocity:" + _playerVelocity );

		if( _tunnelManager ) {
			GUILayout.Label( "section count:" + _tunnelManager.countSection + " time:" + _tunnelManager.countTime );
		}

		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Spwan");
		GUILayout.Label( "spwan objects :" + gos.Length );
	}

	void FixedUpdate() {

		if( _playerState == state.Start ) {

			if( Mathf.Abs( rigidbody.velocity.y ) > _maxFalldownSpeed ) {
				_playerState = state.Falldown;
				Debug.Log ( "state to Falldown" );
			}

			return;
		}

		/*
		if( Input.GetButton("Horizontal") ) {
			
			_playerMove = Input.GetAxis("Horizontal");
			_playerInDirection = _playerMove < 0 ? -1 : 1;

			//rigidbody.velocity = new Vector3( ( in_direction * _playerMoveSpeed ), rigidbody.velocity.y, 0);
			//rigidbody.velocity = new Vector3( in_move, rigidbody.velocity.y, rigidbody.velocity.z );
			
		} else {
		}
		*/

		float h = Input.GetAxis("Horizontal");

		if( h != 0 ) {

			// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if( h * rigidbody.velocity.x < _maxMoveSpeed )
				// ... add a force to the player.
				rigidbody.AddForce( Vector3.right * h * _moveForce);
			
			// If the player's horizontal velocity is greater than the maxSpeed...
			if( Mathf.Abs( rigidbody.velocity.x ) > _maxMoveSpeed )
				// ... set the player's velocity to the maxSpeed in the x axis.
				rigidbody.velocity = new Vector3( Mathf.Sign(rigidbody.velocity.x) * _maxMoveSpeed, 
				                                 rigidbody.velocity.y, rigidbody.velocity.z );

			_playerInDirection = Mathf.Sign( h );
			_lastMoveTime = Time.time;
		} 
		else {

			// freeze minor x-axis velocity
			if( Time.time - _lastMoveTime > 1.0f  ) {

				float freezeVelocity =  Mathf.Abs(rigidbody.velocity.x) / 2;
				if( freezeVelocity < 0.3f )
					freezeVelocity = 0.0f;

				rigidbody.velocity = new Vector3( freezeVelocity * _playerInDirection, 
				                                 rigidbody.velocity.y, rigidbody.velocity.z );
			}
		}

		if( Mathf.Abs( rigidbody.velocity.y ) > _maxFalldownSpeed ) {
			rigidbody.velocity = new Vector3( rigidbody.velocity.x, 
			                                 Mathf.Sign(rigidbody.velocity.y) * _maxFalldownSpeed, rigidbody.velocity.z );
		}

		if( Input.GetKeyUp("up") ) {
			
			rigidbody.AddForce( Vector3.up * _playerGravity * rigidbody.mass, ForceMode.Impulse );
		}

		if( Input.GetKeyUp("down") ) {

			rigidbody.velocity = new Vector3( rigidbody.velocity.x, 
			                                 0, rigidbody.velocity.z );
		}


		/*
		rigidbody.velocity = new Vector3( _playerMove, rigidbody.velocity.y, rigidbody.velocity.z );

		// apply reverse gravity
		//rigidbody.AddForce(Vector3.up * _playerGravity * (rigidbody.mass));
		_playerVelocity = _playerGravity * _UpSpeed;
		rigidbody.AddForce( Vector3.up * _playerVelocity * ( rigidbody.mass ) );

		// max velocity 
		//if( Mathf.Abs(rigidbody.velocity.y) > _maxVelocity ) {
		//	rigidbody.velocity = new Vector3( rigidbody.velocity.x, _maxVelocity * -1, rigidbody.velocity.z );
		//}
		*/
	}

	// Update is called once per frame
	void Update () {

		//float move = _speed * Time.deltaTime;
		//transform.Translate(Vector3.down * move, Space.World);

		//_playerVelocity = _playerGravity * _speed;
		//float move = _playerVelocity * Time.deltaTime;
		//transform.Translate(Vector3.down * move, Space.World);


		//_playerMoveX = Input.GetAxis("Horizontal") * _playerMoveSpeed * Time.deltaTime;
		//_playerMoveY = Input.GetAxis("Vertical");
		//_playerInDirection = _playerMoveX < 0 ? -1 : 1;

		/*
		if( Input.GetKeyDown ( "down" ) ) {
			//_tunnelManager.setSpeed( _tunnelManager.getSpeed() + 1 );
			
			//_playerGravity = _playerGravity - 0.5f;
			//rigidbody.AddForce( 0, -10, 0 );
			
			_UpSpeed++;
			
			//if( _UpSpeed > _maxSpeed )
			//	_UpSpeed = _maxSpeed;
		}
		*/

#if fasle

		// turn on over world size switch
		if( _tunnelManager ) {

			// TODO: setting max world size
			// tigger over position	
			if( transform.position.y < -500 ) {
				_tunnelManager.eventOverWorldSize( this.rigidbody );
			}
		}
#endif

		// max velocity 
		//if( Mathf.Abs(rigidbody.velocity.y) > _maxVelocity ) {
		//	rigidbody.velocity = new Vector3( rigidbody.velocity.x, _maxVelocity * -1, rigidbody.velocity.z );
		//}
	}

	public void playerHit() {

		//animation.Play( "Attack" );

		Animator ani = this.GetComponent<Animator>();
		ani.SetTrigger( "hitSpwan" );

	}

	void OnCollisionEnter(Collision collision) {

		Debug.Log( "player collision : " + collision.gameObject.tag ); 
	}
}
