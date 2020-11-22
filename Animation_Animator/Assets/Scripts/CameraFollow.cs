using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform p_tranform;
    private Vector3 CameraPos;

    void Start()
    {
        p_tranform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CameraPos = transform.position - p_tranform.position;                   
    }

    void Update()
    {
        Vector3 Pos = CameraPos + p_tranform.position;
        transform.position = Vector3.Lerp(transform.position, Pos, 0.05f);        
    }
}
