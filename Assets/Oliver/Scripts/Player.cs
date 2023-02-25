using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float movement_speed;
    [SerializeField] int tile_size;
    Vector3 target_position;
    int score = 0;
    private Animator animator;
    private int totalHulas = 0;

    private void Awake()
    {
        target_position = transform.position;
        animator = GetComponent<Animator>();
    }

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

        if (IsOutOfBounds(target_position))
        {
            target_position = transform.position;
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

    private bool IsOutOfBounds(Vector3 position)
    {
        if (position.x <= Camera.main.ViewportToWorldPoint(Vector3.zero).x)
        {
            return true;
        }
        if(position.x >= Camera.main.ViewportToWorldPoint(Vector3.one).x)
        {
            return true;
        }
        else
            return false;
    }

    public void SetAnimation(Hulahoop.Type type)
    {
        switch (type)
        {
            case Hulahoop.Type.Pink:
                animator.SetBool("collideWithPink", true);
                animator.SetBool("collideWithRed", false);
                animator.SetBool("collideWithViolet", false);
                switch (totalHulas)
                {
                    case 1:
                        animator.SetTrigger("pinkHula");
                        break;
                    case 2:
                        animator.SetTrigger("secondPink");
                        break;
                    default:
                        break;
                }

                break;
            case Hulahoop.Type.Red:
                animator.SetBool("collideWithRed", true);
                animator.SetBool("collideWithPink", false);
                animator.SetBool("collideWithViolet", false);
                switch (totalHulas)
                {
                    case 1:
                        animator.SetTrigger("redHula");
                        break;
                    case 2:
                        animator.SetTrigger("secondRed");
                        break;
                    default:
                        break;
                }
                break;
            case Hulahoop.Type.Violet:
                animator.SetBool("collideWithViolet", true);
                animator.SetBool("collideWithPink", false);
                animator.SetBool("collideWithRed", false);
                switch (totalHulas)
                {
                    case 1:
                        animator.SetTrigger("violetHula");
                        break;
                    case 2:
                        animator.SetTrigger("secondViolet");
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        totalHulas++;
    }

    public void SetAnimationTrigger()
    {
        animator.SetTrigger("collision");
    }
}
