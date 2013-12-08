using UnityEngine;
using System.Collections;

public class BasicScrolling: MonoBehaviour {

	public float _Speed;
	private Material _ScrollMaterial;
	
	void Start()
	{
		//this._ScrollMaterial = renderer.material;
		float xSize=GetComponent<MeshFilter>().mesh.bounds.size.x*transform.localScale.x;
		float ySize=GetComponent<MeshFilter>().mesh.bounds.size.z*transform.localScale.z;

		print ( xSize + " ysize:" + ySize );
	}
	
	void Update()
	{
		//basic scrolling by texture offset
		//this._ScrollMaterial.mainTextureOffset = new Vector2(_Speed * Time.time, 0);
		//this._ScrollMaterial.mainTextureOffset = new Vector2(0,_Speed * Time.time);

		float Move = _Speed * Time.deltaTime;
		transform.Translate(Vector3.down * Move,Space.World);

		if(transform.position.y<-20)
		{
			transform.position = new Vector3(transform.position.x,
			                                 20,transform.position.z);
		}
	}

}