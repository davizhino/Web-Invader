using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shipShoot : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject bulletPrefab;
    public InputActionAsset shootingAction;
    [SerializeField] private int indexSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        shootingAction.FindActionMap("shoot").FindAction("Misil").performed += launchMissil;
    }
    private void OnDestroy()
    {
        shootingAction.FindActionMap("shoot").FindAction("Misil").performed -= launchMissil;
    }


    void launchMissil(InputAction.CallbackContext ctx)
    {
        Debug.Log("Shot");
        indexSpawn = (indexSpawn + 1) % spawnPoints.Count;
        //GameObject lastBullet = Instantiate(bulletPrefab, spawnPoints[indexSpawn].position, Quaternion.identity);
        GameObject lastBullet = Instantiate(bulletPrefab, spawnPoints[indexSpawn].position, Quaternion.identity);
        lastBullet.GetComponent<Bullet>().SetDirection(transform.forward);
        //float shipSpeed = GetComponent<shipMovement>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform t in spawnPoints)
        {
            Gizmos.DrawWireSphere(t.position, 0.0025f);
            Gizmos.DrawSphere(t.position, .0010f);
        }
    }

    private void OnEnable()
    {
        shootingAction.Enable();
    }

    private void OnDisable()
    {
        shootingAction.Enable();

    }
}
