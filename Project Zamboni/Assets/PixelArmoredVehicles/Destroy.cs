using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject exp;
    public GameObject skin;
    public GameObject deadskin;
    private bool isdead = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("dead")&!isdead)
        {
            isdead = true;
            skin.SetActive(false);
            deadskin.SetActive(true);
            Instantiate(exp, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        }
    }
}
