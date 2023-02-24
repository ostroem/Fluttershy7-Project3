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
        if (hoop) {

            player.SetAnimationTrigger();
            player.SetAnimation(hoop.type);
            player.AddScore(HulaHoop);
            scoreText.text = player.GetScore().ToString();
            hoop.gameObject.SetActive(false);
        }
    }
}

