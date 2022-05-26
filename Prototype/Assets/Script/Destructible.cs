using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject destroyedVersion;
    [SerializeField] private Collider JetCollider;

    private void OnCollisionEnter(Collision collision)
    {  
        if(plane.transform.position.y > 10)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            plane.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            plane.SetActive(false);
        }
    }
}