using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Vincent Tse.
 * 2021-02-13
 */

public class BillBoard : MonoBehaviour
{

    public Transform cam;
    
    
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
