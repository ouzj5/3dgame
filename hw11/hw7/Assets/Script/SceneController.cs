using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : UnitySingleton<SceneController>{
	MyActionManager actionManager;
	public bool gameStart = false;
	public SceneController() {
		
	}
	void Awake() {
		//init model, action manager, userGui
		SceneModel.Instance ();					
		actionManager = gameObject.AddComponent<MyActionManager>() as MyActionManager;
		UserGUI.Instance ();
		//load the scene prefabs
		//Instantiate(Resources.Load<GameObject> ("Prefabs/SceneModel"));
		//init factory, and use it to load prefabs
		PatrolFactory.Instance ().load ();
	}
	public MyActionManager getActionManager() {
		return actionManager;
	}
	public void restart() {
		//restart game
		PatrolFactory.Instance ().restart();
		gameStart = true;
	}
}
