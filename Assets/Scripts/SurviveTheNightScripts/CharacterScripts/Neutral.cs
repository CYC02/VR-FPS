using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Cindy Chan
//Inherits from Character
public class Neutral : Character
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gameObject.tag = "Neutral";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
