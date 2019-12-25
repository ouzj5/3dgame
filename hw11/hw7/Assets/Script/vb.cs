using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Vuforia;

public class vb : MonoBehaviour,IVirtualButtonEventHandler {

	// Use this for initialization
	[DllImport("user32.dll", EntryPoint = "keybd_event")]    
	public static extern void keybd_event(             
		byte bVk,    //虚拟键值 对应按键的ascll码十进制值             
		byte bScan,// 0             
		int dwFlags,  //0 为按下，1按住，2为释放             
		int dwExtraInfo  // 0         
		);

	void Start () {
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
	}
	public void OnButtonPressed(VirtualButtonBehaviour vb) {
		print(vb.VirtualButtonName);
		switch (vb.VirtualButtonName) {
			case "vb_left":
				keybd_event(37, 0, 1, 0);
				break;
			case "vb_up":
				keybd_event(38, 0, 1, 0);
				break;
			case "vb_right":
				keybd_event(39, 0, 1, 0);
				break;
			case "vb_down":
				keybd_event(40, 0, 1, 0);
				break;
		}
	}
	// Update is called once per frame
	public void OnButtonReleased(VirtualButtonBehaviour vb) {
        switch (vb.VirtualButtonName) {
			case "vb_left":
				keybd_event(37, 0, 2, 0);
				break;
			case "vb_up":
				keybd_event(38, 0, 2, 0);
				break;
			case "vb_right":
				keybd_event(39, 0, 2, 0);
				break;
			case "vb_down":
				keybd_event(40, 0, 2, 0);
				break;
		}
    }
}
