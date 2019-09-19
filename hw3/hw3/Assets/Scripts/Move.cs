using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move: MonoBehaviour
{
	readonly float move_speed = 20;
	private int move_to_where; //记录移动到那一边， 1 为船上， 2为岸上
	private Vector3 dest;
	private Vector3 middle;
	public static int can_move = 0; // 记录是否可以移动对象，1为不可以，0为可以

	void Update(){
		if (can_move == 1)
			return;
		if(move_to_where == 1){
			transform.position = Vector3.MoveTowards(transform.position, middle, move_speed*Time.deltaTime);
			if (transform.position == middle)
				move_to_where = 2;
		}
		else if(move_to_where == 2){
			transform.position = Vector3.MoveTowards(transform.position, dest, move_speed*Time.deltaTime);
			if (transform.position == dest)
				move_to_where = 0;
		}
	}

	public void SetDestination(Vector3 _dest){
		middle = _dest;
		dest = _dest;
		if (can_move == 1)
			return;
		else{
			if (_dest.y < transform.position.y) {
				middle.y = transform.position.y;
			} else {
				middle.x = transform.position.x;
			}
			move_to_where = 1;
		}
	}
	public void reset(){
		if (can_move == 1)
			return;
		else{
			move_to_where = 0;
		}
	}
}