using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using TMPro;

public class SteamLobby : MonoBehaviour
{
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    //variables 
    public ulong CurrentLobbyID;
    private const string HostAddressKey = "HostAddress";
    private CustomNetworkManager networkManager;

    //GO

    public GameObject HostButton;
    public TMP_Text LobbyNameText;

    private void Start()
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steam is not initialized.");
            return;
        }

        networkManager = GetComponent<CustomNetworkManager>();

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
    {
        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steam is not initialized.");
            return;
        }

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t result)
    {
        if (result.m_eResult == EResult.k_EResultOK)
        {
            Debug.Log("Lobby created successfully!");
            // You can set lobby data here if needed
            networkManager.StartHost();
            SteamMatchmaking.SetLobbyData(new CSteamID(result.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
            SteamMatchmaking.SetLobbyData(new CSteamID(result.m_ulSteamIDLobby), "Name", SteamFriends.GetPersonaName().ToString() + "'s Lobby");
            CurrentLobbyID = result.m_ulSteamIDLobby;
            LobbyNameText.text = "Lobby Name: " + SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "Name");
            }
        else
        {
            Debug.LogError("Failed to create lobby: " + result.m_eResult);
        }
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t result)
    {
        Debug.Log("Received a request to join a lobby: " + result.m_steamIDLobby);
        SteamMatchmaking.JoinLobby(result.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t result)
    {
        Debug.Log("Entered lobby: " + result.m_ulSteamIDLobby);
        // You can retrieve lobby data here if needed

        // Everyone
        HostButton.SetActive(false);
        CurrentLobbyID = result.m_ulSteamIDLobby;
        LobbyNameText.text = "Lobby Name: " + SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "Name");

        // Only clients
        if(NetworkServer.active)
        {
            return;
        }

        networkManager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), HostAddressKey);
        
        networkManager.StartClient();
    }
}
