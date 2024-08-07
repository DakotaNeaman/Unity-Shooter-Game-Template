using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    //Variables to be edited in the editor
    public float damage = 10f;
    public float fireRate = 15f;
    public float range = 100f;
    public float ADSTime = 0.25f;

    public static int staticCurrentAmmo;
    public static int staticMaxAmmo;

    public int maxAmmo = 30;
    public float reloadTime = 0.75f;

    public float sensitivityModifier = 1f;

    public float standardFOV = 60f;
    public float currentFOV;

    public LayerMask acceptedLayers;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public GameObject gun;
    public GameObject cloneContainer;
    public GameObject prefab;
    public Animator animator;

    public AudioSource shootingSound;
    public AudioSource reloadSound;

    public GameObject scopeOverlay;
    public bool usesScope = false;

    public bool fullAuto = false;

    private int currentAmmo; 
    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    private bool isAiming = false;
    //private bool scopeShowing = false;



    void Start() {
        currentAmmo = maxAmmo;
        currentFOV = fpsCam.fieldOfView;
        fpsCam.fieldOfView = standardFOV;
    }

    void OnEnable() {
        isReloading = false;
        animator.SetBool("Reloading", false);
        StopADS();
        if(Input.GetButton("Aim")) {
            StartCoroutine(StartADS());
        }
    }

    void OnDisable() {
        if(usesScope) {
            scopeOverlay.SetActive(false);
            prefab.SetActive(true);
        }
    }

      IEnumerator Reload() {
            isReloading = true;
            animator.SetBool("Reloading", true);
            reloadSound.Play();

            yield return new WaitForSeconds(reloadTime - 0.25f);
            animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(0.25f);

            currentAmmo = maxAmmo;
            isReloading = false;

            if(Input.GetButton("Aim")) {
                StartCoroutine(StartADS());
            }
        }

        IEnumerator StartADS() {
            StopADS();
            isAiming = true;
            MouseLook.mouseSensitivity = MouseLook.mouseSensitivity / sensitivityModifier;
            animator.SetBool("StartAiming", true);
            yield return new WaitForSeconds(ADSTime);
            animator.SetBool("StartAiming", false);
            yield return null;
        }

          void StopADS() {
            StopCoroutine(StartADS());
            isAiming = false;
                if(usesScope) {
                    scopeOverlay.SetActive(false);
                    prefab.SetActive(true);
                }
            MouseLook.mouseSensitivity = MouseLook.defaultMouseSensitivity;
            animator.SetBool("StartAiming", false);
            animator.Play("Idle");
            currentFOV = standardFOV;
        }

        void Shoot() {
                if(isAiming == false || usesScope == false) {
            muzzleFlash.Play();
                }
            currentAmmo--;

            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, acceptedLayers)) {
                //Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();

                if(target != null){
                    target.TakeDamage(damage);
                }

                Enemy enemy = hit.transform.GetComponent<Enemy>();

                if(enemy != null){
                    enemy.TakeDamage(damage);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.transform.parent = cloneContainer.transform;
                impactGO.name = "(Particles)";
                Destroy(impactGO, 2f);
            }

        }

    void Update() {

        if(isReloading){
            fpsCam.fieldOfView = standardFOV;
            return;
        }

        if(Pause.paused) {
            fpsCam.fieldOfView = standardFOV;
            StopADS();
            return;
        }

        if(usesScope) {
            if(isAiming) {
                scopeOverlay.SetActive(true);
                prefab.SetActive(false);
            } else {
                scopeOverlay.SetActive(false);
                prefab.SetActive(true);
            }
        }

        staticCurrentAmmo = currentAmmo;
        staticMaxAmmo = maxAmmo;

        fpsCam.fieldOfView = currentFOV;

        if(currentAmmo <= 0 || (Input.GetButtonDown("Reload") && currentAmmo != maxAmmo)) {
            StopADS();
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButtonDown("Aim")) {
            StartCoroutine(StartADS());
        }

        if(Input.GetButtonUp("Aim")) {
            StopADS();
        }
        
        if(fullAuto) {
            if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 60f/fireRate;
                Shoot();
                shootingSound.Play();
            }
        } else {
            if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 60f/fireRate;
                Shoot();
                shootingSound.Play();
            }
        }
    }
}