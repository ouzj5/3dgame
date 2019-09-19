using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController {
	BoatModel model;
	readonly Move move;

	public BoatController(){
		model = new BoatModel ();
		move = model.getGameObject().AddComponent (typeof(Move)) as Move;

	}
	public void getOnBoat(CharacterController cha){
		int index = model.getEmptyIndex ();
		model.setPassenger (index, cha);
	}

	public CharacterController getOffBoat(string object_name){
		return model.getPassenger(object_name);
	}

	public void boatMove(){
		if (model.getTFflag() == 1) {
			move.SetDestination (model.getToPos());
			model.setTFflag (-1);
		} else {
			move.SetDestination (model.getFromPos());
			model.setTFflag (1);
		}
	}

	public GameObject getGameObject() {
		return model.getGameObject ();
	}
	public BoatModel getModel() {
		return model;
	}
	public void reset(){
		move.reset ();
		if (model.getTFflag() == -1) {
			boatMove ();
		}
		model.reset ();

	}
}
