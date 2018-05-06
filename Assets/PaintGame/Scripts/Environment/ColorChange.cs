using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
public class ColorChange : MonoBehaviour {

    public float duration = 3;

    new Light light;

    Color oldColor;
    Color newColor;

    float lastTime;

    void Start()
    {
        light = GetComponent<Light>();
        oldColor = light.color;
        newColor = Random.ColorHSV();
    }

	void Update () {
        var time = Time.time;
        var sub = time - lastTime;
        if(sub > duration)
        {
            oldColor = newColor;
            newColor = Random.ColorHSV();
            lastTime = time;
            sub = 0;
        }
        light.color = Color.Lerp(oldColor, newColor, sub / duration);
	}
}
