using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringBehavior : MonoBehaviour
{
    public float speed = 0.01f;
    public score score;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        score.count++;
        score.soundTrigger = true;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(speed, 0f, 0f);
    }
}
