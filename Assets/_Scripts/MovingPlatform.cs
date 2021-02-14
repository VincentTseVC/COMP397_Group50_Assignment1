using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.gameObject.tag == "Player")
            other.gameObject.transform.parent = null;
    }


}
