using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int HulaHoop = 0;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hulahoop hoop = collision.gameObject?.GetComponent<Hulahoop>();
        if (hoop.transform.rotation.eulerAngles.z <= 0.5f && player.GetIsShaking()) {
            player.SetAnimationTrigger();
            player.SetAnimation(hoop.type);
            player.AddScore(HulaHoop);
            scoreText.text = player.GetScore().ToString();
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

