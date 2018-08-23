using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    private bool[] wasEnabled;

    public void Setup()
    {

        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {

            wasEnabled[i] = disableOnDeath[i].enabled;

        }

        SetDefaults();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
    /**
	void Update () {

        if (!isLocalPlayer)
        {

            return;

        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            RpcTakeDamage(1000);

        }
		
	}
    **/

    public void SetDefaults()
    {

        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {

           disableOnDeath[i].enabled = wasEnabled[i];

        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

    }

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {

        if (isDead)
        {
            return;
        }

        currentHealth -= amount;
        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth <=0)
        {
            Die();
        }

    }

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {

            disableOnDeath[i].enabled = false;

        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        Debug.Log(transform.name + " died!");

        // Call Respawn Method
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {

        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        Debug.Log("I'm back baby!");

    }
}
