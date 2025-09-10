using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Slider healthBar;
    Player player;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        healthBar.value = player.GetHealth();
    }
}
