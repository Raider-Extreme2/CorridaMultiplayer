using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class FinalPoints : MonoBehaviour
{
    public RaceUI raceUI;

    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            raceUI = other.GetComponent<RaceUI>();
        }
        if (raceUI.points > 50)
        {
            player1Text.text = "Jogador 1.: " + raceUI.points.ToString("F0") + " Pontos";
            player2Text.text = "Jogador 2.: não terminou";
        }
        else
        {
            return;
        }
        
    }
}
