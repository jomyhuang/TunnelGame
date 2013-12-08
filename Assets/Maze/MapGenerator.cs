using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject wallPrefab;
	public GameObject rockPrefab;
	private int default_x_max = 19;
	private int default_z_max = 19;
	
	// Use this for initialization
	void Start () {
		//这是地图元件的数据
		string map_matrix  = "022520000000000000:022220000000000000:022250000000000000:025251111111111110:022220000000000010:000100000000000010:000110000000000010:000010022500000010:000010032200000010:000010052200000010:000011152200000010:000000022200000010:000000000000000010:000000000000002522:000000000000015222:000000000000002222:000000000000000000:000000000000000000";
		
		//wakucreate
		CreateMapWaku();
		
		// 在这里加入参数生成地图
		CreateMap(map_matrix);
	}
	
	void CreateMapWaku()
	{
		for(int dx = 0; dx <= default_x_max; dx++){
			Instantiate(wallPrefab, new Vector3(dx, 0, 0), Quaternion.identity);
			Instantiate(wallPrefab, new Vector3(dx, 0, default_z_max), Quaternion.identity);
		}
		
		for(int dz = 0; dz <= default_z_max; dz++){
			Instantiate(wallPrefab, new Vector3(0, 0, dz), Quaternion.identity);
			Instantiate(wallPrefab, new Vector3(default_x_max, 0, dz), Quaternion.identity);
		}
		
	}

	void CreateMap(string map_matrix)
	{
		string[] map_matrix_arr = map_matrix.Split(':');
		
		for(int x = 0; x < map_matrix_arr.Length; x++){
			string x_map = map_matrix_arr[x];
			for(int z = 0; z < x_map.Length; z++){
				int obj = int.Parse(x_map.Substring(z, 1));
				if(obj == 0){
					Instantiate(rockPrefab, new Vector3(x + 1, 0, z  + 1), Quaternion.identity);
				}
			}
		}
		
	}
}
