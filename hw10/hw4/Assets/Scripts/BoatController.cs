using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController {
	BoatModel model;
	MySceneActionManager actionManager;
	public BoatController(MySceneActionManager am){
		actionManager = am;
		model = new BoatModel ();

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
			//move.SetDestination (model.getToPos());
			actionManager.moveBoat(model.getGameObject(), model.getToPos(), 20);
			model.setTFflag (-1);
		} else {
			//move.SetDestination (model.getFromPos());
			actionManager.moveBoat(model.getGameObject(), model.getFromPos(), 20);
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
		if (model.getTFflag() == -1) {
			boatMove ();
		}
		model.reset ();

	}
}
