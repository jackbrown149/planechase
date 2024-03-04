using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : NetworkBehaviour
{

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    [ClientRpc]
    public void RpcRoll()
    {
        gameObject.SetActive(true);
        transform.position = transform.position + new Vector3(0, 0, 0);
        body.angularVelocity = new Vector3(((Random.value) * 360f) + 60, ((Random.value) * 360f) + 60, ((Random.value) * 360f) + 60);
        body.velocity = new Vector3((Random.value - 0.5f) * 4f, 7f, (Random.value - 0.5f) * 4f);
    }
}
