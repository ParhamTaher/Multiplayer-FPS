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

    [SerializeField]
    private string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject playerGraphics;

    [SerializeField]
    GameObject playerUIPrefab;

    private GameObject PlayerUIInstance;
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

            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            PlayerUIInstance = Instantiate(playerUIPrefab);
            PlayerUIInstance.name = playerUIPrefab.name;

        }

        GetComponent<Player>().Setup();

    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {

            SetLayerRecursively(child.gameObject, newLayer);

        }

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

        Destroy(PlayerUIInstance);

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
