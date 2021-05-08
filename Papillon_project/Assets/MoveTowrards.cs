using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowrards : MonoBehaviour
{
    [SerializeField] private GameObject viewingPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, viewingPos.transform.position, Time.deltaTime * 5);
    }
}
