using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SectionTrigger_obs : MonoBehaviour
{

    public GameObject obstacle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(obstacle, new Vector3(-6, 0, 40), Quaternion.identity);
        }
    }

}

