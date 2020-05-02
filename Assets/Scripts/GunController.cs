using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public int damage = 2;
    public float range = 100f;
    public float fireRate = 15f;
    public float nextFire = 0f;
    
    [SerializeField]
    private int ammoPerClip = 50;
    public Camera  playerCam;
    public ParticleSystem muzzleFlash;
    private PersistenceController pc;
    private UIController ui;
    private Animator anim;

    private AudioSource shootingSound;

    private AudioSource reloadSound;

    private bool reloading = false;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        pc = PersistenceController.Instance;
        ui = GameObject.Find("UIController").GetComponent<UIController>();
        shootingSound = GetComponents<AudioSource>()[0];
        reloadSound = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        //If in main menu, do nothing
        if(!pc.inGame) {
            return;
        }

        if(Input.GetKeyDown("r") && pc.ammoLeft != 0){
            StartCoroutine("Reload");
        }

        //Fire weapon
        if(Input.GetMouseButton(0) && Time.time >= nextFire && !anim.GetCurrentAnimatorStateInfo(0).IsName("WeaponReload") && pc.ammoInClip != 0)
        {
            shootingSound.Play();
            nextFire = Time.time + 1f/fireRate;
            pc.ammoInClip -= 1;
            ui.UpdateUI();

            muzzleFlash.Play();
            RaycastHit hitInfo;
            if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo, range)) {
                if(hitInfo.collider.gameObject.tag == "Enemy") {
                    UnityEngine.Debug.Log("Hit");
                    hitInfo.collider.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                }
                
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
        if(reloading || pc.ammoInClip == ammoPerClip) {
            yield break;
        }
        reloading = true;
        reloadSound.Play();
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
        reloading = false;
               
    }

}
