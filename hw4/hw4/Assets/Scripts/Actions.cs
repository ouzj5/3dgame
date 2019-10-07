using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject    
{
	public GameObject gameobject;  //动作对象
	public Transform transform;    	//动作对象的transform
	public ISSActionCallback callback;     //回调函数

    public bool enable = true;     //是否正在运行
    public bool destroy = false;   //是否需要被销毁

    public virtual void Start() {
        
    }

    public virtual void Update() {
        
    }
}



public enum SSActionEventType : int { Started, Competeted }

public interface ISSActionCallback {
    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null);
}







