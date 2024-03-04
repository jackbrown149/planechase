using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : NetworkBehaviour
{

    public bool stackable = true;

    [SyncVar]
    public int cardId = -1;

    public Sprite dungeon, oubliette, spell;

    private CardUpdator updator;
    private MeshRenderer renderer;

    private void Start()
    {
        updator = GetComponentInChildren<CardUpdator>();
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    bool first = true;
    private void Update()
    {
        if (first)
        {
            first = false;
            return;
        }
        if (cardId != -1)
        {
            UpdateCard(cardId);
        }
    }

    public void UpdateCard(int id)
    {
        if (updator == null)
        {
            updator = GetComponentInChildren<CardUpdator>();
            renderer = GetComponentInChildren<MeshRenderer>();
        }
        cardId = id;
        updator.UpdateID(id);

        CardInfo info = CardManager.GetInfo(id);
        var r = renderer.materials;

        if (info.type.Equals("Dungeon"))
        {
            r[0].mainTexture = dungeon.texture;
        }
        else if (info.type.Equals("Spell"))
        {
            r[0].mainTexture = spell.texture;
        }
        else
        {
            r[0].mainTexture = oubliette.texture;
        }

        renderer.materials = r;

        //this.sprite = sprite;
        //this.spriteId = id;
        //MeshRenderer renderer = GetComponent<MeshRenderer>();
        //var r = renderer.materials;

        //r[0] = new Material(r[0]);
        //r[0].color = Color.white;
        //r[0].mainTexture = sprite.texture;

        //renderer.materials = r;
    }

    [Command(requiresAuthority = false)]
    public void CmdDelete()
    {
        Destroy(gameObject);
    }

}
