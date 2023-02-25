using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] int tile_size;
    [SerializeField] int score = 0;
    [SerializeField] float movement_speed;
    [SerializeField] private int totalHulasEquipped = 0;
    private Vector3 target_position;
    private Animator animator;
    bool isShaking = false;
    float elapsedTimeSinceShake = 0.0f;

    private void Awake()
    {
        target_position = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement();
        shake_body();
    }

    public bool GetIsShaking() { return isShaking; }
    private void shake_body()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            isShaking = true;
            animator.SetTrigger("shakingBody");
            elapsedTimeSinceShake = 0.0f;
        }
        else {
            elapsedTimeSinceShake += Time.deltaTime;
            if(elapsedTimeSinceShake >= 0.32f)
            {
                isShaking = false;
            }

        }
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
            target_position.x = Mathf.FloorToInt(transform.position.x);
            return;
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
        if (position.x < Camera.main.ViewportToWorldPoint(Vector3.zero).x)
        {
            return true;
        }
        if(position.x > Camera.main.ViewportToWorldPoint(Vector3.one).x)
        {
            return true;
        }
        else
            return false;
    }

    public void SetAnimation(Hulahoop.Type type)
    {
        if(totalHulasEquipped < 3)
        {
            totalHulasEquipped++;
        }
        switch (type)
        {
            case Hulahoop.Type.Pink:
                animator.SetBool("collideWithPink", true);
                animator.SetBool("collideWithRed", false);
                animator.SetBool("collideWithViolet", false);
                switch (totalHulasEquipped)
                {
                    case 1:
                        animator.SetTrigger("pinkHula");
                        break;
                    case 2:
                        animator.SetTrigger("secondPink");
                        break;
                    case 3:
                        animator.SetTrigger("thirdPink");
                        break;
                    default:
                        break;
                }
                break;
            case Hulahoop.Type.Red:
                animator.SetBool("collideWithRed", true);
                animator.SetBool("collideWithPink", false);
                animator.SetBool("collideWithViolet", false);
                switch (totalHulasEquipped)
                {
                    case 1:
                        animator.SetTrigger("redHula");
                        break;
                    case 2:
                        animator.SetTrigger("secondRed");
                        break;
                    case 3:
                        animator.SetTrigger("thirdRed");
                        break;
                    default:
                        break;
                }
                break;
            case Hulahoop.Type.Violet:
                animator.SetBool("collideWithViolet", true);
                animator.SetBool("collideWithPink", false);
                animator.SetBool("collideWithRed", false);
                switch (totalHulasEquipped)
                {
                    case 1:
                        animator.SetTrigger("violetHula");
                        break;
                    case 2:
                        animator.SetTrigger("secondViolet");
                        break;
                    case 3:
                        animator.SetTrigger("thirdViolet");
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

    }

    public void SetAnimationTrigger()
    {
        animator.SetTrigger("collision");
    }
}
