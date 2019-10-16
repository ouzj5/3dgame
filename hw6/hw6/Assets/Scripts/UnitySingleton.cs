
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour where T : Component {
	//用于单例实例化
	protected static T _instance;

	public static T Instance() {
		if (_instance == null) { 
			_instance = FindObjectOfType(typeof(T)) as T;
			if (_instance == null) {
				GameObject obj = new GameObject();
				obj.name = typeof(T).ToString();
				_instance = obj.AddComponent<T>();
			}
		}
		return _instance;
	}
}