using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;
    [SerializeField] Button startButton;

    private void Awake()
    {
        //Adicionando funcoes do NetworkManager aos botoes na cena
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            DisableButtons();
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            DisableButtons();
        });
    }

    public void DisableButtons()
    {
        inputField.interactable = false;
        hostButton.interactable = false;
        clientButton.interactable = false;

        startButton.interactable = true;
    }

    public void LoadGameScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
