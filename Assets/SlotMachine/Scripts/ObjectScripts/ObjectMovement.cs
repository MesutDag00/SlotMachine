using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public BetCounter BetCounter;
    public bool ActiveSpawns;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == this.gameObject.name && ActiveSpawns == true)
            Destroy(this.gameObject.transform.GetComponent<Rigidbody2D>());
        if (collision.gameObject.name == "DestroyWall" && ActiveSpawns == false)
            Destroy(this.gameObject);
    }
}

public enum BetCounter
{
    Elements500Buy = (int)1.2f,
    Elements1kBuy = (int)3.5f,
    Elements2kBuy = 6,
    Elements3kBuy = (int)9.5f
}