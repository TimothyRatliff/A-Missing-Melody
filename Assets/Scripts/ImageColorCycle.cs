using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorCycle : MonoBehaviour
{
    private Color lerpedColor = Color.white;
    private Image image;
    public Color white = new Color(1, 1, 1, .96f);
    public Color green = new Color(0, 1, 0, .96f);
    public int slowIndex = 6;

    void Start()
    {
        image = transform.GetComponent<Image>();
    }

    void Update()
    {
        lerpedColor = Color.Lerp(white, green, Mathf.PingPong(Time.time/slowIndex, 1));
        image.color = lerpedColor;
    }
}
