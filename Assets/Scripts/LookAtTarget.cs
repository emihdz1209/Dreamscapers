using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
     public Transform target;
     public Vector3 rotationOffset = new Vector3(0, 90, 0);

     void Start()
     {
          if (target == null)
        {
            GameObject playerObj = GameObject.Find("PlayerObject");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player Cam not found! Please assign playerOrientation manually.");
            }
        }
     }
     void Update()
     {
          if(target != null)
          {
               transform.LookAt(target);
               transform.Rotate(rotationOffset);
          }
     }
}
