using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int hulaPoints = 1;
    [SerializeField] private Player player;
    private Hulahoop lastHulahoop;
    [SerializeField] bool easyCollision = false;

    private void Awake()
    {
        player.easyCollision = easyCollision;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hulahoop hoop = collision.gameObject?.GetComponent<Hulahoop>();
        if (easyCollision && player.TotalHulas < 3 && lastHulahoop != hoop)
        {
            player.SetAnimationTrigger();
            player.SetAnimation(hoop);
            player.AddScore(hulaPoints);
            player.MissedHulas = 0;
            hoop.gameObject.SetActive(false);
            return;
        }
        if (hoop.transform.rotation.eulerAngles.z <= 0.5f && player.GetIsShaking() && player.TotalHulas < 3 && lastHulahoop != hoop) {
            player.SetAnimationTrigger();
            player.SetAnimation(hoop);
            player.AddScore(hulaPoints);
            player.MissedHulas = 0;
            hoop.gameObject.SetActive(false);
            return;
        }
        lastHulahoop = hoop;
        player.MissedHulas++;

        float random = Random.Range(0, 2);
        if(random == 0)
        {
            hoop.GetComponent<Rigidbody2D>().MoveRotation(-30);
        }
        else
        {
            hoop.GetComponent<Rigidbody2D>().MoveRotation(30);
        }

    }
}

