using Mirror;
using UnityEngine;

public class Interactable : NetworkBehaviour
{

    private PlayerMovement heldBy = null;

    private Rigidbody body;

    public bool canRotate = true;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    [Server]
    public void Update()
    {
        if (isHeld())
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
        if (transform.position.y < -10) transform.position = new Vector3(0, 10, 0);
    }

    [ClientRpc]
    public void RpcMove(Vector3 position, Vector3 rotation)
    {
        transform.position = position;
        if (canRotate) transform.eulerAngles = rotation;
        //Debug.Log(transform.position + "   " + transform.eulerAngles);
    }

    [ClientRpc]
    public void RpcSetHeld(PlayerMovement heldBy)
    {
        this.heldBy = heldBy;

    }

    [ClientRpc]
    public void RpcDisableBody()
    {
        body.useGravity = false;
    }

    [ClientRpc]
    public void RpcEnableBody()
    {
        body.useGravity = true;
    }

    [ClientRpc]
    public void RpcLaunch(Vector3 velocity)
    {
        body.velocity = velocity;
    }

    public bool isHeld()
    {
        return heldBy != null;
    }

    public PlayerMovement GetHeldBy()
    {
        return heldBy;
    }

}
