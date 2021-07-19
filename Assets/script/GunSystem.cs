using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform camaraHead;//camera control

    public GameObject bullet;
    public Transform firePosition;
    public GameObject muzzleFlash, bulletHold;

    public float shootingRate;

    public bool canAutoFire;
    private bool shooting, readyToShoot = true;

    public int bulletCount, totalBullet, MagSize;

    // Start is called before the first frame update
    void Start()
    {
        totalBullet -= MagSize;
        bulletCount = MagSize;
    }

    // Update is called once per frame
    void Update()
    {

        Shoot();
        GunManage();
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
                        Instantiate(bulletHold, hit.point, Quaternion.LookRotation(hit.normal));
                }

            }
            else
            {
                firePosition.LookAt(camaraHead.position + camaraHead.forward * 50f);
            }

            Instantiate(muzzleFlash, firePosition.position, firePosition.rotation, firePosition);
            Instantiate(bullet, firePosition.position, firePosition.rotation, firePosition);

            StartCoroutine(ResetShot());

            bulletCount--;
        }
    }

    private void Reload()
    {
        int bulletToAdd = MagSize - bulletCount;

        if(totalBullet > bulletToAdd)
        {
            totalBullet -= bulletToAdd;
            bulletCount = MagSize;
        }
        else
        {
            totalBullet = 0;
            bulletCount += totalBullet;
        }
    }



    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(shootingRate);

        readyToShoot = true;
    }
}
