using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : ISingleton<Debugger>
{
    public Text log;
    // Start is called before the first frame update
    void Start()
    {
        log = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
