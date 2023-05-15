using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakBrickController : MonoBehaviour
{

    public Tilemap blocks;

    private MarioController marioController;

    private void Start()
    {
        marioController = GetComponent<MarioController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector3 contact = collision.contacts[0].point;
        Vector3Int tile = blocks.WorldToCell(contact);
        if (collision.gameObject.CompareTag("Mario"))
        {

            Debug.Log(tile.x + " caca " + tile.y + 1);
            if (marioController.isMarioBig)
            { 
                Vector3Int tileDefinitiva = new Vector3Int(tile.x, tile.y + 1);
                Debug.Log(tileDefinitiva);
                blocks.SetTile(tileDefinitiva, null);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
