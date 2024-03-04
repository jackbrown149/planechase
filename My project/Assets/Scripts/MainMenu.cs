using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private NetworkManager manager;

    public Text textBox, textBox2;

    private void Start()
    {
        manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();


        if (SteamManager.Initialized)
        {
            lobbyCreated = Callback<LobbyCreated_t>.Create(LobbyCreated);
            joinRequest = Callback<GameLobbyJoinRequested_t>.Create(JoinRequest);
            lobbyEntered = Callback<LobbyEnter_t>.Create(LobbyEntered);
        }

    }

    public void Join()
    {
        manager.networkAddress = textBox.text;
        //manager.networkAddress = "localhost";
        manager.StartClient();
    }

    public void Quit()
    {
        Application.Quit();
    }

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> joinRequest;
    protected Callback<LobbyEnter_t> lobbyEntered;

    public void Host()
    {
        //CardManager.amount = int.Parse(textBox2.text);
        //manager.StartHost();
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, manager.maxConnections);
    }

    public void LobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult == EResult.k_EResultOK)
        {
            manager.StartHost();

            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "ServerIP", SteamUser.GetSteamID().ToString());
        }
    }

    private void JoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void LobbyEntered(LobbyEnter_t callback)
    {
        if (!NetworkServer.active)
        {
            string address = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "ServerIP");
            manager.networkAddress = address;
            manager.StartClient();
        }
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

}
