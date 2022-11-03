using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{

    public int damageAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {


        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<HealthSystem>().DamageEnemy(damageAmount);

        }

    }
    
        
    
}
