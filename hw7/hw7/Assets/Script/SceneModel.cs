using System;
using System.Collections.Generic;
using UnityEngine;


public class SceneModel : UnitySingleton<SceneModel>{
	float verFence = 12.6f;
	float leftFence = -3f;
	float rightFence = 3f;
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

