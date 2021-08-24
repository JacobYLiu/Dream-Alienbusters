using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform camaraHead;//camera control

    //ammo text
    private UI_controller UI_canvas;

    public GameObject Grenade;
    public GameObject bullet;
    public GameObject trail;
    public Transform firePosition;

    public GameObject muzzleFlash, bulletHole;
    

    public float shootingRate;

    public bool canAutoFire;
    private bool shooting, readyToShoot = true;

    public int bulletCount, totalBullet, MagSize;
    public int damage = 40;

    //animation
    public Animator animator_control;

    public float reLoadEmptyTime;
    public float reloatNotEmptyTime;
    private float reLoadTime;
    public int max_ammo;



    public float ammo_pick_up_cool_down = 300;
    private bool reday_to_add_ammo = true;
    private bool reloading = false;


    public bool Luancher;

    [Tooltip("The audio clip that is played while shooting."), SerializeField]
    public AudioClip sound;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        totalBullet -= MagSize;
        bulletCount = MagSize;
    
        UI_canvas = FindObjectOfType<UI_controller>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) { return; }

        Shoot();
        GunManage();
        update_ammo_count();

    }

    private void play_gun_sound()
    {

    }



    private void update_ammo_count()
    {
        UI_canvas.total_bullet.SetText(totalBullet.ToString());
        UI_canvas.ammoCount.SetText(bulletCount + "/" + MagSize);
        
    }

    private void GunManage()
    {
        if(Input.GetKeyDown(KeyCode.R) && bulletCount < MagSize && !reloading)
        {
            reloading = true;
            Reload();
        }
    }


    private void Shoot()
    {
        if (canAutoFire)
        {
            shooting = Input.GetMouseButton(0);
        }
        else{
            shooting = Input.GetMouseButtonDown(0);
        }
        if (shooting && readyToShoot && bulletCount > 0)
        {
            readyToShoot = false;
            RaycastHit hit;

            if (Physics.Raycast(camaraHead.position, camaraHead.forward, out hit, 100f))
            {
                if (Vector3.Distance(camaraHead.position, hit.point) > 1f)
                {
                    firePosition.LookAt(hit.point);

                    if (!Luancher)
                    {
                        if (hit.collider.tag == "Shootable")
                            Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                    }


                }
                if (hit.collider.CompareTag("Enemy") && !Luancher)
                {
                    hit.collider.GetComponent<enemyHealthSystem>().TakingDamange(damage);
                }
                if (hit.collider.CompareTag("Target") && hit.collider.GetComponent<TargetScript>().isHit == false && !Luancher)
                {
                    hit.collider.GetComponent<TargetScript> ().isHit = true;
                    hit.collider.GetComponent<Animation>().Play("target_down");
                }
            }
            else
            {
                firePosition.LookAt(camaraHead.position + camaraHead.forward * 50f);
            }

            if (!Luancher)
            {
                Instantiate(muzzleFlash, firePosition.position, firePosition.rotation, firePosition);
                Instantiate(bullet, firePosition.position, firePosition.rotation, firePosition);
            }
            else{
                Instantiate(bullet, firePosition.position, firePosition.rotation);
                Instantiate(trail, firePosition.position, firePosition.rotation);
            }

            audio.PlayOneShot(audio.clip);
            animator_control.SetTrigger("Shoot");

            StartCoroutine(ResetShot());

            bulletCount--;
        }
        
    }

    public void addAmmo()
    {
        if (reday_to_add_ammo) 
        {
            reday_to_add_ammo = false;
            totalBullet = max_ammo;
            StartCoroutine(AmmoPickCD());
        }
    }

    private void Reload()
    {
        if (bulletCount == 0 && totalBullet > 0)
        {

            reLoadTime = reLoadEmptyTime;//3f;
            readyToShoot = false;
            animator_control.SetTrigger("reload_empty");
        }
        else if (bulletCount > 0 && totalBullet > 0)
        {
            readyToShoot = false;
            reLoadTime = reloatNotEmptyTime;//2.03f;
            animator_control.SetTrigger("reload_not_empty");
        }



        StartCoroutine(ReloadCoroutine(reLoadTime));
        
    }

    IEnumerator AmmoPickCD()
    {
        yield return new WaitForSeconds(ammo_pick_up_cool_down);

        reday_to_add_ammo = true;
    }


    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(shootingRate);

        readyToShoot = true;
    }

    IEnumerator ReloadCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);

        int bulletToAdd = MagSize - bulletCount;

        reloading = false;

        if (totalBullet > bulletToAdd)
        {
            totalBullet -= bulletToAdd;
            bulletCount = MagSize;
        }
        else
        {
            bulletCount += totalBullet;
            totalBullet = 0;
        }
        readyToShoot = true;
    }
}
