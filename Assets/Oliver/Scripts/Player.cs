using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{

    [SerializeField] private int totalHulasEquipped = 0;
    [SerializeField] int score = 0;
    [SerializeField] int timesShaken = 0;
    [SerializeField] int pointsForColorCombo = 10;
    [SerializeField] private TMP_Text scoreText;
    private float movement_speed = 5;
    private Vector3 target_position;
    private Animator animator;
    bool isShaking = false;
    float elapsedTimeSinceLastShake = 0.0f;
    float timeToRemoveHula = 0.0f;
    List<Hulahoop> hulas;
    public int TotalHulas { get => totalHulasEquipped; }
    public bool easyCollision = false;

    private void Awake()
    {
        target_position = transform.position;
        animator = GetComponent<Animator>();
        hulas = new List<Hulahoop>();
    }

    void Update()
    {
        movement();
        shake_body();
        checkHulas();
        update_score_text();

        if (want_to_remove_hula())
        {
            remove_last_hula();
        }
    }

    private void update_score_text()
    {
        scoreText.text = score.ToString();
    }

    private void checkHulas()
    {
        if(totalHulasEquipped >= 3)
        {
            if (hulas.TrueForAll(i => i.type == Hulahoop.Type.Pink))
            {
                score += pointsForColorCombo;
                animator.SetTrigger("removeAllPink");
                totalHulasEquipped = 0;
                hulas.Clear();
            }
            else if (hulas.TrueForAll(i => i.type == Hulahoop.Type.Red))
            {
                score += pointsForColorCombo;
                animator.SetTrigger("removeAllRed");
                totalHulasEquipped = 0;
                hulas.Clear();
            }
            else if (hulas.TrueForAll(i => i.type == Hulahoop.Type.Red))
            {
                score += pointsForColorCombo;
                animator.SetTrigger("removeAllViolet");
                totalHulasEquipped = 0;
                hulas.Clear();
            }
        }
    }

    public bool GetIsShaking() { return isShaking; }
    private void shake_body()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShaking = true;
            animator.SetTrigger("shakingBody");
            elapsedTimeSinceLastShake = 0.0f;

            timesShaken++;
        }
        else
        {
            if (easyCollision)
            {
                return;
            }


            elapsedTimeSinceLastShake += Time.deltaTime;
            timeToRemoveHula += Time.deltaTime;
            if (timeToRemoveHula > 0.5f)
            {
                timeToRemoveHula = 0.0f;
                timesShaken = 0;
            }
            if (elapsedTimeSinceLastShake >= 1.6f)
            {
                elapsedTimeSinceLastShake = 0;
                totalHulasEquipped = 0;
                animator.SetTrigger("stoppedShaking");

                hulas.Clear();
                isShaking = false;
            }
        }
    }

    private void remove_last_hula()
    {
        Debug.Log("remove last hula");
        timesShaken = 0;
        totalHulasEquipped--;
        animator.SetTrigger("removeHula");
        hulas.RemoveAt(hulas.Count - 1);
    }

    private bool want_to_remove_hula()
    {
        if (timesShaken > 2 && totalHulasEquipped >= 3)
        {
            return true;
        }
        else return false;
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
            target_position.x = Mathf.RoundToInt(transform.position.x);
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

    public void SetAnimation(Hulahoop hula)
    {
        if(totalHulasEquipped >= 3)
        {
            Debug.Log("too many hulas");
            return;
        }

        totalHulasEquipped++;
        hulas.Add(hula);

        switch (hula.type)
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
