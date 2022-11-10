using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    public int damageAmount = 1;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<BossHealth>().DamageBoss(damageAmount);
        }
    }

}
