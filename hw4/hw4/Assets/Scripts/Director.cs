using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director: System.Object
{
	private static Director _instance;
	public SceneController curren{ get; set;}
	public static Director get_Instance(){
		if (_instance == null)
		{
			_instance = new Director();
		}
		return _instance;
	}
}
public interface SceneController
{
	void loadResources();
}
public interface UserAction{
	void moveboat();
	void isClickChar (CharacterController tem_char);
	void restart();
	void setMove(int v);
}






