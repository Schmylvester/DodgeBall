using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] Text m_playerCount;
    [SerializeField] Button hostButton;
    [SerializeField] Button joinButton;

    void hideUI()
    {
        hostButton.gameObject.SetActive(false);
        joinButton.gameObject.SetActive(false);
    }

    public void StartHost()
    {
        hideUI();
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        hideUI();
        NetworkManager.singleton.StartClient();
    }
}
