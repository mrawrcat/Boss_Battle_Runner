using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair_Move : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector2 screen_bounds;
    private float obj_width;
    private float obj_height;
    // Start is called before the first frame update
    void Start()
    {
        screen_bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        obj_width = player.transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        obj_height = player.transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    
    private void LateUpdate()
    {
        Vector3 viewpos = transform.position;
        viewpos.x = Mathf.Clamp(viewpos.x, screen_bounds.x * -1 + obj_width, screen_bounds.x - obj_width);
        viewpos.y = Mathf.Clamp(viewpos.y, screen_bounds.y * -1 + obj_height, screen_bounds.y - obj_height);
        transform.position = viewpos;
    }
}
