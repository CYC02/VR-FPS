using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Cindy Chan
//Inherits from Character
public class Player : Character
{
    public WinLoseScreen screen;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (health == 0) {
            screen.ShowLoseScreen();
        }
    }
}
