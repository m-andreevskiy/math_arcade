using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] private Transform transformToFollowPosition;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = transformToFollowPosition.position + offset;
    }
}
