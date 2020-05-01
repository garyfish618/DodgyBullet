using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float nextFire = 0f;
    
    [SerializeField]
    private int ammoPerClip = 50;
    public Camera  playerCam;
    public ParticleSystem muzzleFlash;
    public PersistenceController pc;
    public UIController ui;

    private Animator anim;


    void Start()
    {
        anim = transform.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r") && pc.ammoLeft != 0){
            StartCoroutine("Reload");
        }

        //Fire weapon
        if(Input.GetMouseButton(0) && Time.time >= nextFire && !anim.GetCurrentAnimatorStateInfo(0).IsName("WeaponReload") && pc.ammoInClip != 0)
        {
            nextFire = Time.time + 1f/fireRate;
            pc.ammoInClip -= 1;
            ui.UpdateUI();

            muzzleFlash.Play();
            RaycastHit hitInfo;
            if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo, range)) {
                
            }
        }

        // Aim down sight
        if(Input.GetMouseButtonDown(1)) {
            if(anim.GetBool("WeaponSight")) {
                anim.SetBool("WeaponSight", false);
                return;
            }

            anim.SetBool("WeaponSight", true);
        }

    }


    private IEnumerator Reload() {
        //Return if ammo is full or reload already going 
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("WeaponReload") || pc.ammoInClip == ammoPerClip) {
            yield break;
        }

        anim.SetTrigger("Reload");
        yield return new WaitForSeconds(2.5f);

        int bulletsNeeded = ammoPerClip - pc.ammoInClip;

        if(pc.ammoLeft < bulletsNeeded) {
            pc.ammoInClip += pc.ammoLeft;
            pc.ammoLeft = 0;
        }

        else {
            pc.ammoInClip += bulletsNeeded;
            pc.ammoLeft -= bulletsNeeded;
        }

        ui.UpdateUI();
               
    }

}
