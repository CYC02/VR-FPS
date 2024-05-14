using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int damage = 10;
    public AudioSource attackSound;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
