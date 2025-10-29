using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caminou_movement1 : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= new Vector3(0, 0, 10) * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destruir"))
        {
            Destroy(gameObject);
        }
    }
}
