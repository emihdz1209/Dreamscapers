using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
     public Transform target;
     public Vector3 rotationOffset = new Vector3(0, 90, 0);

     void Update()
     {
          if(target != null)
          {
               transform.LookAt(target);
               transform.Rotate(rotationOffset);
          }
     }
}
