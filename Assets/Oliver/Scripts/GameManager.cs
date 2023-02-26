using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    private Hulahoop_Falling hulahoop;
    public Hulahoop_Falling Hulahoop_Falling { get => hulahoop; }
    public static GameManager Instance { get => gameManager; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }

        gameManager = this;
        hulahoop = FindObjectOfType<Hulahoop_Falling>();
    }
}
