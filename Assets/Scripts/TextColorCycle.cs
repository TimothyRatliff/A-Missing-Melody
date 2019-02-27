using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorCycle : MonoBehaviour
{
    private Color lerpedColor = Color.white;
    private Text text;
    public Color color1 = new Color(0.5f, 0.5f, 0.5f, 1);
    public Color color2 = new Color(1, 1, 1, 1);
    public int slowIndex = 2;

    void Start()
    {
        text = transform.GetComponent<Text>();
    }

    void Update()
    {
        lerpedColor = Color.Lerp(color1, color2, Mathf.PingPong(Time.time/slowIndex, 1));
        text.color = lerpedColor;
    }
}
