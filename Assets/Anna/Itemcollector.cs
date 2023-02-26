using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int hulaPoints = 1;
    [SerializeField] private Player player;

    [SerializeField] bool easyCollision = false;

    private void Awake()
    {
        player.easyCollision = easyCollision;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hulahoop hoop = collision.gameObject?.GetComponent<Hulahoop>();
        if (easyCollision && player.TotalHulas < 3)
        {
            player.SetAnimationTrigger();
            player.SetAnimation(hoop);
            player.AddScore(hulaPoints);
            hoop.gameObject.SetActive(false);
        }
        if (hoop.transform.rotation.eulerAngles.z <= 0.5f && player.GetIsShaking() && player.TotalHulas < 3) {
            player.SetAnimationTrigger();
            player.SetAnimation(hoop);
            player.AddScore(hulaPoints);
            hoop.gameObject.SetActive(false);
            return;
        }



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

