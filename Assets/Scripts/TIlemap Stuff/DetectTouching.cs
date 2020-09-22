using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTouching : MonoBehaviour
{

    public Vector2 booga;
    public string tilename;
    public LayerMask layer;
    public float tilemap_length;
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + booga.x,transform.position.y + booga.y), Vector2.left, Mathf.Infinity, layer);
        if(hit.collider != null)
        {
            tilename = hit.collider.name;
            if(hit.collider.tag == "Tilemap")
            {
                if(transform.position.x > 0)
                {
                    if (transform.position.x != hit.collider.gameObject.transform.position.x + tilemap_length)
                    {
                        transform.position = new Vector2(hit.collider.gameObject.transform.position.x + tilemap_length, transform.position.y);
                    }
                }
            }
            else if(hit.collider.tag != "Tilemap")
            {
                print("not hitting tilemap");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector2(transform.position.x + booga.x, transform.position.y + booga.y), Vector2.left);
    }
}
