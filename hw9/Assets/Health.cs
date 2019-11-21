using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
	// 当前血量
	public float health = 1f;
	// 增/减后血量
	private float resulthealth;
	public Slider healthSlider;


	void Start()
	{
		resulthealth = health;
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(105, 80, 40, 20), "加血"))
		{
			resulthealth = resulthealth + 0.1f > 1.0f ? 1.0f : resulthealth + 0.1f;
		}
		if (GUI.Button(new Rect(155, 80, 40, 20), "扣血"))
		{
			resulthealth = resulthealth - 0.1f < 0.0f ? 0.0f : resulthealth - 0.1f;
		}

		//插值计算health值，以实现血条值平滑变化
		health = Mathf.Lerp(health, resulthealth, 0.05f);
		healthSlider.value = health;

		// 用水平滚动条的宽度作为血条的显示值
		GUI.HorizontalScrollbar(new Rect(50, 50, 200, 20), 0.0f, health, 0.0f, 1.0f);
	}
}