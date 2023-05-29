using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoost : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private bool isBoosted = false;
    public int boosterLevel = 0; // 0(yok) 1(basit) 2(orta) 3(guclu) 4(debug)
    private Rigidbody2D rb;
    private Transform transform;
    private Vector2 startMove;
    public float boostSpeed = 1200f; //levele göre baslangic ivmelenme hizini ayarliyor.
    public Color JumpColor = Color.blue;
    private SpriteRenderer render1;

    [SerializeField] private AudioSource boostSound;

    


    // Start is called before the first frame update
    void Start()
    {
        SetBoosterLevel();
        rb = rocket.GetComponent<Rigidbody2D>();
        transform = rocket.GetComponent<Transform>();
        startMove = new Vector2(0, boostSpeed);
        render1 = GetComponent<SpriteRenderer>();

        





    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") != 0 && isBoosted ==false)
        {
            boostSound.Play();
            rb.AddForce(startMove);
            isBoosted = true;
            ColorChange();

        }
    }

    private void ColorChange()
    {
        render1.color = JumpColor;
    }
    public void SetBoosterLvl(int boosterLvl)
    {
        boosterLevel = boosterLvl;
    }
    public void SetBoosterLevel()
    {
        if (boosterLevel == 0)
        {
            boostSpeed = 0f;
        }
        else if (boosterLevel == 1)
        {
            boostSpeed = 400f;
            
        }
        else if (boosterLevel == 2)
        {
            boostSpeed = 800f;
            
        }
        else if (boosterLevel == 3)
        {
            boostSpeed = 1200f;
            
        }
        else
        {
            Debug.Log("Boost Speed ayarlanmadi");
        }
    }
}
