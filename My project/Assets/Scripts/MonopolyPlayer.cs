using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonopolyPlayer : NetworkBehaviour
{

    [SyncVar]
    public int coins = 10;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private GameObject coin, coinSpawn;

    private void Update()
    {
        coinText.text = "" + coins;

        if (isServer)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Coin"))
        {
            Destroy(other.gameObject);
            AddCoin();
        }
    }

    public void AddCoin()
    {
        //coins++;
        CmdAddCoin();
    }

    public void RemoveCoin()
    {
        //coins--;
        CmdRemoveCoin();
    }

    public void DropCoin()
    {
        //Instantiate(coin, coinSpawn.transform.position, coinSpawn.transform.rotation);
        //CmdRemoveCoin();
        //NetworkServer.Spawn()
        CmdDropCoin();
    }

    [Command(requiresAuthority = false)]
    private void CmdRemoveCoin()
    {
        coins--;
    }

    [Command(requiresAuthority = false)]
    private void CmdAddCoin()
    {
        coins++;
    }

    [Command(requiresAuthority = false)]
    private void CmdDropCoin()
    {
        if (coins > 0)
        {
            GameObject objec = Instantiate(coin, coinSpawn.transform.position, coinSpawn.transform.rotation);
            NetworkServer.Spawn(objec);
            coins--;
        }
    }

    [ClientRpc]
    private void RpcDropCoin()
    {
        Instantiate(coin, coinSpawn.transform.position, coinSpawn.transform.rotation);
    }

}
