using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var Movement = new float3();
        Movement.x = Input.GetAxisRaw

        ("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        transform.Translate(Movement * 10f * Time.deltaTime);
    }
}
