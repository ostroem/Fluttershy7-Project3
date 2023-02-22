using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float movement_speed = 1.0f;
    [SerializeField] int tile_size = 10;
    Vector3 target_position;

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement() {
        Vector3 new_pos = Vector3.MoveTowards(transform.position, target_position, Time.deltaTime * movement_speed);


    }
}
