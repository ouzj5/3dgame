using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour{
	PatrolModel model;						//model
	float speed;							//speed of the patrol
	public bool chase = false;				//the flag of chasing player
	bool done;								//use as flag of action complete 
	public void Awake(){
		done = true;
		speed = 0.05f;
		model = new PatrolModel (this.gameObject);
	}
	public void addMove() {
		if (!chase) {						//random to move
			Vector3 target = model.getNextPos ();
			SceneController.Instance ().getActionManager ().moveBoat (model.getObject (), target, speed);
		} else {							//chase the player
			Vector3 target = model.getTargetPos ();
			SceneController.Instance().getActionManager().moveBoat (model.getObject (), target, speed);
		}
	}
	public void Update() {
		if (done) {
			addMove ();
			done = false;
		}
	}
	public void send() {
		//use for create a new movemenet
		done = true;
	}
	void OnCollisionStay(Collision e) {
		if (e.gameObject.name.Contains ("Patrol") || e.gameObject.name.Contains ("fence")
		    || e.gameObject.tag.Contains ("FenceAround")) {
			//chose next to move
			send ();
		} else if (e.gameObject.name.Contains ("Player")) {
			//gg
			UserGUI.Instance ().gameOver ();
		}
	}
	public void setChase(bool chase) {
		//set the chase mode
		this.chase = chase;
	}
}