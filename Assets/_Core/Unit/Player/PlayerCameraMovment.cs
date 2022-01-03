using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraMovment : MonoBehaviour
{
    private void Update()
    {
        if (!Player.Instance)
            return;

        var target = Player.Instance.transform;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
