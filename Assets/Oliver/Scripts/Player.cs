using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float movement_speed;
    [SerializeField] int tile_size;
    Vector3 target_position;
    int score = 0;

    private void Awake()
    {
        target_position = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            target_position.x -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            target_position.x += 1;
        }

        Vector3 new_pos = Vector3.MoveTowards(transform.position, target_position, Time.deltaTime * movement_speed);
        transform.position = new_pos;

    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return score;
    }
}
