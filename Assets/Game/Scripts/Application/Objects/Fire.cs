using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float speed = 360f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward,speed * Time.deltaTime);
    }
}
