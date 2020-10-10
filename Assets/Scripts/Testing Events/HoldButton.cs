using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{

    public Text counttxt;
    private float counter;
    private bool holding_down;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counttxt.text = counter.ToString("F0");
        if (holding_down)
        {
            counter += 1 * Time.deltaTime;
        }
    }

    public void Increment_Counter()
    {
        holding_down = true;
    }

    public void Stop_Counter()
    {
        holding_down = false;
    }

}
