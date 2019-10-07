using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoastModel {
	private CharacterController[] character; //岸上的人物对象
	readonly Vector3[] postion;	//岸上位置的坐标
	readonly Vector3 from_pos = new Vector3(9,1,0); //from 岸的位置
	readonly Vector3 to_pos = new Vector3(-9,1,0);  //to 岸的位置
	GameObject coast;  //岸游戏对象
	int TFflag;  //1: from， -1: to， 用于判断是那一边的岸

	public CoastModel() {
		postion = new Vector3[] {
			new Vector3 (6.5f, 2.25f, 0),
			new Vector3 (7.5f, 2.25f, 0),
			new Vector3 (8.5f, 2.25f, 0),
			new Vector3 (9.5f, 2.25f, 0),
			new Vector3 (10.5f, 2.25f, 0),
			new Vector3 (11.5f, 2.25f, 0)
		};
		character = new CharacterController[6];
	}

	public GameObject CreateCoast(int side) {
		if(side == 1){
			coast = Object.Instantiate(Resources.Load("Prefabs/Mycoast", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
			coast.name = "from";
			TFflag = 1;
		}
		else{
			coast = Object.Instantiate(Resources.Load("Prefabs/Mycoast", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
			coast.name = "to";
			TFflag = -1;
		}
		return coast;
	}
	public int getTFflag(){
		return TFflag;
	}

	public int getIndexByName(string name) {
		for(int i=0; i < character.Length; i++){
			if(character[i] != null && character[i].isName(name)){
				return i;
			}
		}
		return -1;
	}
	public int getEmptyIndex(){
		for(int i=0; i < character.Length; i++){
			if(character[i] == null){
				return i;
			}
		}
		return -1;
	}
	public Vector3 getEmptyPosition(){
		int index = getEmptyIndex();
		Vector3 pos = postion[index];
		pos.x *= TFflag;
		return pos;
	}
	public Vector3 getMidPosition() {
		Vector3 pos = postion[0];
		pos.x *= TFflag;
		return pos;
	}
	public void getOnCoast(CharacterController myCharacter){
		int index = getEmptyIndex();
		character[index] = myCharacter;
	}
	public int[] getCharacterNum(){
		int[] count = {0,0};
		for(int i=0; i<character.Length; i++){
			if(character[i] == null) continue;
			if(character[i].isType(0)) count[0]++;
			else count[1]++;
		}
		return count;
	}
	public void setCharacter(int index, CharacterController cha) {
		character[index] = cha;
	}
	public void reset(){
		character = new CharacterController[6];
	}
}