using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int timer;
    public int speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.gameObject, timer);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.name == "Wall" || obj.gameObject.name == "Tilemap")
        {
            Destroy(transform.gameObject);
        }
    }
}
