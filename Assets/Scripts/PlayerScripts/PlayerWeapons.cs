using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    public float damage = 1f;

    public GameObject bulletPrefab;
    public GameObject Bullet;
    public GameObject GunModel;

    Vector3 bulletDirection;

    public GameObject muzzle;
    public Camera camera;

    public GameObject gun;
    public bool GunActive;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        //RangeFinder();
        if (GunActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (transform.GetComponent<PlayerStats>().ammo > 0)
                {
                    Shoot();
                    SoundManager.Instance.PlayClip("gunshot1");
                }
            }
        }
    }

    //void Shoot()
    //{
    //    Bullet = GameObject.Instantiate(bulletPrefab);
    //    Bullet.transform.position = muzzle.transform.position;
    //    Bullet.name = "Bullet";
    //
    //    Bullet.GetComponent<Rigidbody>().AddForce(camera.transform.forward * 4000f);
    //    //Invoke("CleanUp", 3);
    //    StartCoroutine(CleanUp(4f, Bullet));
    //}

    void Shoot()
    {
        transform.GetComponent<PlayerStats>().ReducePlayerAmmo();

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {


            RangeFinder();

            VirusEnemyStats virusenemystats = hit.transform.GetComponent<VirusEnemyStats>();

            if (virusenemystats != null)
            {
                virusenemystats.TakeDamage(damage);

            }

            BossHealth bossenemystats = hit.transform.GetComponent<BossHealth>();

            if (bossenemystats != null)
            {

                bossenemystats.ReduceHealth(damage);
            }


            DataContainerObject datacontainer = hit.transform.GetComponent<DataContainerObject>();

            if (datacontainer != null)
            {
                datacontainer.Drop();
            }



        }
    }

    void RangeFinder()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            //Debug.Log(hit.distance);
            bulletDirection = hit.transform.position - camera.transform.position;
            Vector3 actualDirection = camera.transform.forward;

            Bullet = GameObject.Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.LookRotation(hit.point));
            //Bullet.transform.position = muzzle.transform.position;
            Bullet.name = "Bullet";

            //bulletDirection = 

            Bullet.GetComponent<Rigidbody>().AddForce(actualDirection * 10000f);
            StartCoroutine(CleanUp(1f, Bullet));

        }
    }


    IEnumerator CleanUp(float destroyAfter, GameObject thisBullet)
    {

        yield return new WaitForSeconds(destroyAfter);
        Destroy(thisBullet);


    }

    public void GunRotation()
    {

    }

    public void PickUpGun()
    {
        GunActive = true;
    }
}
