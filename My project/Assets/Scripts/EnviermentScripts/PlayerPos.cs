using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{

    private GameMaster gm;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

        var cc = Player.GetComponent<CharacterController>();

        cc.enabled = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
        cc.enabled = true;
    }

    // Update is called once per frame
    void Update()
    { if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
