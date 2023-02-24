using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int HulaHoop = 0;
    [SerializeField] private TMP_Text bananasText;
    [SerializeField] private Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HulaHoop"))
        {
            player.AddScore(HulaHoop);
            bananasText.text = player.GetScore().ToString();
        }
    }
}

