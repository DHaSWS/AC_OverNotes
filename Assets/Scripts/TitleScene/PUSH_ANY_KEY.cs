using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PUSH_ANY_KEY : MonoBehaviour
{
    public Vector3 Position;
    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Position.x, Mathf.Sin(Time.time) * 0.7f + Position.y, Position.z);
    }
}
