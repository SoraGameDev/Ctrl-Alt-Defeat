using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDScript : MonoBehaviour
{
    public Sprite[] AmmoSprites;
    public Sprite[] HPSprites;

    public Image Ammo;
    public Image HP;

    public int hp;
    public int ammo;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hp = player.GetComponent<PlayerStats>().health;
        ammo = player.GetComponent<PlayerStats>().ammo;

        Ammo.sprite = AmmoSprites[ammo];
        HP.sprite = HPSprites[hp/10];
    }
}
