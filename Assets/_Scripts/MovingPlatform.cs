using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Vincent Tse.
 * 2021-02-13
 */


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
