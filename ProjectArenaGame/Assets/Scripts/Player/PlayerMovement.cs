using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    void Update()
    {
        if (!isLocalPlayer) return;

        var Movement = new float3();
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        transform.Translate(Movement * 10f * Time.deltaTime);
    }
}