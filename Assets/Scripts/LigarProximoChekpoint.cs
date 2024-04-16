using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigarProximoChekpoint : MonoBehaviour
{
    [SerializeField] GameObject nextChekpoint;
    public RaceUI raceUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            raceUI = other.GetComponent<RaceUI>();
            nextChekpoint.SetActive(true);
            gameObject.SetActive(false);

            raceUI.AtualizarPontos();
            raceUI.elapsedTime = 0;
        }
    }
}
