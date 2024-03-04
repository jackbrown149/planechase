using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{

    public static PlayerMovement _main;

    private CharacterController controller;

    private Camera camera;

    private Rigidbody holding = null;

    private Interactable held = null;

    [SerializeField]
    private GameObject desired;

    private bool canLook = true;

    private bool equipped = false;

    private GameObject menu;
    private UIHand hand;

    private void Start()
    {
        if (isLocalPlayer)
        {
            _main = this;
            Camera.main.transform.parent = transform;
            Camera.main.transform.localPosition = new Vector3(0, 1.73f, -0.01f);
            camera = transform.GetComponentInChildren<Camera>();
            controller = GetComponent<CharacterController>();
            desired.transform.parent = camera.transform;

            Cursor.lockState = CursorLockMode.Locked;

            menu = GameObject.Find("Pause Menu");
            menu.SetActive(false);

            NetworkManager nm = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            
            for (int i = 0; i < menu.transform.childCount; i++)
            {
                Transform form = menu.transform.GetChild(i);
                if (form.name.Equals("Back"))
                {
                    form.GetComponent<Button>().onClick.AddListener(() => {
                        menu.SetActive(false);
                        Cursor.lockState = CursorLockMode.Locked;
                    });
                }
                else if (form.name.Equals("Quit"))
                {
                    form.GetComponent<Button>().onClick.AddListener(() => {
                        if (isServer) nm.StopHost();
                        else nm.StopClient();
                        SceneManager.LoadScene("Menu");
                    });
                }
            }

            hand = GameObject.Find("Hand UI").GetComponent<UIHand>();
        }
    }

    [Client]
    private void Update()
    {
        if (!authority) return;
        Menu();
        if (menu.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.LeftAlt)) Cursor.lockState = CursorLockMode.None;
        if (Input.GetKeyUp(KeyCode.LeftAlt)) Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKey(KeyCode.LeftAlt)) return;
        CameraZoom();

        Movement();

        if (!equipped) Interacting();

        //Gun();

        //Interacting();
    }

    private void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            if (menu.activeSelf) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void CameraZoom()
    {
        if (Input.GetMouseButton(1))
        {
            camera.fieldOfView -= 150f * Time.deltaTime;
            if (camera.fieldOfView < 20) camera.fieldOfView = 20;
        }
        else
        {
            camera.fieldOfView += 150f * Time.deltaTime;
            if (camera.fieldOfView > 90) camera.fieldOfView = 90;
        }
    }

    private void Movement()
    {
        // Head Rotate

        float mouseY = Input.GetAxis("Mouse Y");

        if (canLook) camera.transform.Rotate(new Vector3(-mouseY, 0, 0) * Settings.sensitivity);

        // Limit camera rotation
        //0.7071068   -0.7071068
        Quaternion rot = camera.transform.localRotation;
        if (rot.x > 0.7071068) rot.x = 0.7071068f;
        else if (rot.x < -0.7071068) rot.x = -0.7071068f;
        camera.transform.localRotation = rot;

        // Body Rotate

        float mouseX = Input.GetAxis("Mouse X");

        if (canLook) transform.Rotate(new Vector3(0, mouseX, 0) * Settings.sensitivity);

        // Movement

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 moveX = vertical * transform.forward;
        Vector3 moveZ = horizontal * transform.right;

        Vector3 t = moveX + moveZ;
        t = t.normalized * Time.deltaTime * 3.75f;
        if (Input.GetKey(KeyCode.LeftShift) && vertical >= 0 /*&& horizontal == 0*/) t *= 2;

        bool wasGrounded = controller.isGrounded;
        CollisionFlags l = controller.Move(t + new Vector3(0, -0.3f, 0));
    }

    private void Interacting()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Ray(camera.transform.position, camera.transform.forward), out hit, 4f, 1 << 8))
        {
            if (Input.GetMouseButton(0) && held == null)
            {
                held = hit.collider.GetComponent<Interactable>();
                if (!held.isHeld())
                {
                    desired.transform.position = held.transform.position;
                    desired.transform.rotation = held.transform.rotation;

                    CmdDisableRigidInteractable(held);
                    CmdSetHeldBy(held);
                }
            }
            else if (!Input.GetMouseButton(0) && held != null)
            {
                //held.transform.SetParent(null);

                Vector3 right = camera.transform.right * Input.GetAxis("Mouse X") * 3;
                Vector3 up = camera.transform.up * Input.GetAxis("Mouse Y") * 4;

                CmdLaunchInteractable(held, right + up);

                CmdEnableRigidInteractable(held);
                CmdRemoveHeldBy(held);

                held = null;
            }
            else if (hit.collider.tag.Equals("Dice") && held == null && Input.GetKeyDown(KeyCode.R))
            {
                CmdRoll(hit.collider.GetComponent<Dice>());
            }
            else if (hit.collider.tag.Equals("Card Stack") && held == null)
            {
                if (Input.GetMouseButtonDown(2))
                {
                    hit.collider.GetComponent<CardStack>().CmdRemoveTop();
                } else if (Input.GetKeyDown(KeyCode.R))
                {
                    hit.collider.GetComponent<CardStack>().CmdShuffleCards();
                } else if (Input.GetKeyDown(KeyCode.F))
                {
                    //Card c = hit.collider.GetComponent<CardStack>().GetTop();
                    //if (c != null)
                    //{
                    //    hand.AddCard(c);
                    //    c.CmdDelete();
                    //}

                    int i = hit.collider.GetComponent<CardStack>().GetTop();
                    if (i != -1) hand.AddCard(i);
                }
            }
            if (hit.collider.tag.Equals("Card") && Input.GetKeyDown(KeyCode.F)) // 50   40
            {
                hit.collider.transform.Rotate(Vector3.up, 180);
            }
        }
        else if (held != null && !Input.GetMouseButton(0))
        {
            //held.transform.SetParent(null);

            Vector3 right = camera.transform.right * Input.GetAxis("Mouse X") * 3;
            Vector3 up = camera.transform.up * Input.GetAxis("Mouse Y") * 4;

            CmdLaunchInteractable(held, right + up);
            CmdEnableRigidInteractable(held);
            CmdRemoveHeldBy(held);

            held = null;
        }
        if (held != null)
        {
            if (Input.GetMouseButton(2))
            {
                canLook = false;
                //desired.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
                desired.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
                desired.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y"));
            } else
            {
                canLook = true;
            }
            held.transform.position = desired.transform.position;
            held.transform.eulerAngles = desired.transform.eulerAngles;
            CmdMoveInteractable(held, desired.transform.position, desired.transform.eulerAngles);
        } else
        {
            canLook = true;
        }
    }

    [ClientRpc]
    public void RpcHit()
    {
        if (authority)
        {
            Debug.Log("MOVING");
            transform.position = new Vector3(0, 2, 0);
        }
    }

    [Command]
    public void CmdRoll(Dice dice)
    {
        dice.RpcRoll();
    }

    [Command]
    public void CmdMoveInteractable(Interactable interactable, Vector3 position, Vector3 rotation)
    {
        if (interactable == null) return;
        interactable.RpcMove(position, rotation);
    }

    [Command]
    public void CmdLaunchInteractable(Interactable interactable, Vector3 velocity)
    {
        if (interactable == null) return;
        interactable.RpcLaunch(velocity);
    }

    [Command]
    public void CmdDisableRigidInteractable(Interactable interactable)
    {
        if (interactable == null) return;
        interactable.RpcDisableBody();
    }

    [Command]
    public void CmdEnableRigidInteractable(Interactable interactable)
    {
        if (interactable == null) return;
        interactable.RpcEnableBody();
    }

    [Command]
    public void CmdSetHeldBy(Interactable interactable)
    {
        if (interactable == null) return;
        if (!interactable.isHeld()) interactable.RpcSetHeld(this);
    }

    [Command]
    public void CmdRemoveHeldBy(Interactable interactable)
    {
        if (interactable == null) return;
        if (interactable.isHeld() && interactable.GetHeldBy() == this) interactable.RpcSetHeld(null);
    }

}
