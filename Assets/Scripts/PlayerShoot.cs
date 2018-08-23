using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    public PlayerWeapon weapon;

	// Use this for initialization
	void Start () {

        if (cam == null)
        {

            Debug.Log("No Camera Referenced");
            this.enabled = false;

        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {

            Shoot();

        } 
		
	}

    [Client]
    private void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {

            if (hit.collider.tag == "Player")
            {

                CmdPlayerShot(hit.collider.name, weapon.damage);

            }

        }

    }

    [Command]
    private void CmdPlayerShot (string playerID, int damage)
    {

        Debug.Log(playerID + " has been shot");
        Player player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(damage);
    }
}
