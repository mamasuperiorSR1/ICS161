using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject destroyedVersion;
    [SerializeField] private Collider JetCollider;
    [SerializeField] private bool destroyed = false;


    private void OnCollisionEnter(Collision collision)
    {  
        if(plane.transform.position.y > 100 & !destroyed)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            plane.SetActive(false);
            destroyed = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) & !destroyed)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            plane.SetActive(false);
            destroyed = true;
        }
        if(plane.transform.position.y < 0 & !destroyed)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            plane.SetActive(false);
            destroyed = true;
        }
    }
}