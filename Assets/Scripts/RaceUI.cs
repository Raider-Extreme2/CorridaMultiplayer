using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RaceUI : MonoBehaviour
{
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI posText;

    public GameObject player;
    public Rigidbody playerRB;

    private void Awake()
    {
        player.GetComponent<CarController>();
        playerRB.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var vel = playerRB.velocity;
        var speed = vel.magnitude;

        spdText.text = vel.ToString();
    }
}
