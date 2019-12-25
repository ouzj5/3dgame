using System;
using System.Collections.Generic;
using UnityEngine;


public class SceneModel : UnitySingleton<SceneModel>{
	float verFence = 11.5f;
	float leftFence = -4.2f;
	float rightFence = 3.7f;
	public SceneModel () {
		
	}
	public int getAreaIndex(Vector3 pos) {
		if (pos.z >= verFence) {
			if (pos.x < leftFence)
				return 0;
			else if (pos.x > rightFence)
				return 2;
			else
				return 1;
		}
		else {
			if (pos.x < leftFence)
				return 3;
			else if (pos.x > rightFence)
				return 5;
			else
				return 4;
		}
	}

}

