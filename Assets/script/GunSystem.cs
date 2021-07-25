using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform camaraHead;//camera control

    //ammo text
    private UI_controller UI_canvas;

    public GameObject bullet;
    public Transform firePosition;

    public GameObject muzzleFlash, bulletHole;


    public float shootingRate;

    public bool canAutoFire;
    private bool shooting, readyToShoot = true;

    public int bulletCount, totalBullet, MagSize;

    //animation
    public Animator animator_control;

    public float reLoadTime;

    // Start is called before the first frame update
    void Start()
    {
        totalBullet -= MagSize;
        bulletCount = MagSize;

        UI_canvas = FindObjectOfType<UI_controller>();
    }

    // Update is called once per frame
    void Update()
    {

        Shoot();
        GunManage();
        update_ammo_count();
    }

    private void update_ammo_count()
    {
        UI_canvas.total_bullet.SetText(totalBullet.ToString());
        UI_canvas.ammoCount.SetText(bulletCount + "/" + MagSize);
    }

    private void GunManage()
    {
        if(Input.GetKeyDown(KeyCode.R) && bulletCount < MagSize)
        {
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
                if (Vector3.Distance(camaraHead.position, hit.point) > 2f)
                {
                    firePosition.LookAt(hit.point);
                    if (hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                }

            }
            else
            {
                firePosition.LookAt(camaraHead.position + camaraHead.forward * 50f);
            }

            Instantiate(muzzleFlash, firePosition.position, firePosition.rotation, firePosition);
            Instantiate(bullet, firePosition.position, firePosition.rotation, firePosition);

            animator_control.SetTrigger("Shoot");
            StartCoroutine(ResetShot());

            bulletCount--;
        }
    }

    private void Reload()
    {
        if (bulletCount == 0)
        {
            reLoadTime = 3;
            animator_control.SetTrigger("reload_empty");
        }
        else
        {
            reLoadTime = 2.03f;
            animator_control.SetTrigger("reload_not_empty");
        }



        StartCoroutine(ReloadCoroutine());
    }



    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(shootingRate);

        readyToShoot = true;
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reLoadTime);

        int bulletToAdd = MagSize - bulletCount;



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
    }
}
