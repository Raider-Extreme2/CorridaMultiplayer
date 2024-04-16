using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;

public class RaceUI : NetworkBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI posText;

    public float elapsedTime;
    public float points;
    public NetworkVariable<float> sincPoints = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        timerText.text = "Time: " + elapsedTime.ToString("F1");
    }

    public void AtualizarPontos() 
    {
        if (!IsOwner)
        {
            return;
        }
        points += 1000 / elapsedTime;
        sincPoints.Value = points;
        pointsText.text = "Pontos: " + points.ToString("F0");
    }
}
