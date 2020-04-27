using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float nextFire = 0f;
    public Camera  playerCam;
    public ParticleSystem muzzleFlash;
    public PersistenceController pc;
    public UIController ui;
    // Update is called once per frame
    void Update()
    {
        //Fire weapon
        if(Input.GetMouseButton(0) && Time.time >= nextFire && pc.ammoInClip != 0)
        {
            nextFire = Time.time + 1f/fireRate;
            pc.ammoInClip -= 1;
            ui.UpdateUI();

            muzzleFlash.Play();
            RaycastHit hitInfo;
            if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo, range)) {
                
            }
        }

    }
}
