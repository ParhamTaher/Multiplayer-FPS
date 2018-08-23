using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    private Camera sceneCamera;

	// Use this for initialization
	void Start () {

        if (!isLocalPlayer)
        {

            DisableComponents();
            AssignRemtoeLayer();

        } else
        {
           sceneCamera = Camera.main;
           if (sceneCamera != null)
            {

                sceneCamera.gameObject.SetActive(false);

            }

        }

        GetComponent<Player>().Setup();

    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(netID, player);
    }

    private void OnDisable()
    {

        if (sceneCamera != null)
        {

            sceneCamera.gameObject.SetActive(true);

        }

        GameManager.UnRegisterPlayer(transform.name);

    }

    // Update is called once per frame
    void Update () {
		
	}

    void DisableComponents()
    {

        for (int i = 0; i < componentsToDisable.Length; i++)
        {

            componentsToDisable[i].enabled = false;

        }

    }

    void AssignRemtoeLayer()
    {

        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);

    }


}
