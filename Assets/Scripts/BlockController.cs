using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockController : MonoBehaviour
{

    public Tilemap blocks;
    public TileBase roto;

    
    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector3 contact = collision.contacts[0].point;
        Vector3Int tile = blocks.WorldToCell(contact) ;

        if (collision.gameObject.CompareTag("Mario")) {
           
            Debug.Log(tile.x + " caca " + tile.y +1);
            Vector3Int tileDefinitiva = new Vector3Int(tile.x, tile.y+1);
            Debug.Log(tileDefinitiva);


            if(blocks.GetTile(tileDefinitiva) != null) 
                blocks.SetTile(tileDefinitiva, roto);
            
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
