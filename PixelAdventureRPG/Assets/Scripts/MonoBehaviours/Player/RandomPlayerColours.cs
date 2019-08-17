using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerColours : MonoBehaviour
{
    public SpriteRenderer[] PlayerSprites;
    private Sprite[] localPlayerSprites = new Sprite[5]; 
    public Sprite[] hairStyles;
    private bool hasHelmet = false;
    private Color hairColor;
    private Color shirtColor;
    private Color shoeColor;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        changeColor();
        player = GetComponentInParent<Player>();

    }

    private void changeColor()
    {
        for (int i = 0; i < PlayerSprites.Length; i++)
        {
            localPlayerSprites[i] = PlayerSprites[i].sprite;
            PlayerSprites[i].color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
        changeHair();


    }

    public void changeHair()
    {
        if (!hasHelmet)
        {
            PlayerSprites[3].sprite = hairStyles[Random.Range(0, hairStyles.Length)];
            localPlayerSprites[3] = PlayerSprites[3].sprite;
        }
    }

    public void setHelmet(Sprite helmet)
    {
        Debug.Log("SetHelmet");
        player.amor += 0.2f;
        player.healthBar.ActivateArmor(0);
        hasHelmet = true;
        PlayerSprites[3].sprite = helmet;
        hairColor = PlayerSprites[3].color;
        PlayerSprites[3].color = Color.white;
    }

    public void setArmor(Sprite armor)
    {
        player.amor += 0.2f;
        player.healthBar.ActivateArmor(1);
        PlayerSprites[2].sprite = armor;
        shirtColor = PlayerSprites[2].color;
        PlayerSprites[2].color = Color.white;

    }

    public void setShoes(Color color)
    {
        player.amor += 0.2f;
        player.healthBar.ActivateArmor(2);
        shoeColor = PlayerSprites[0].color;
        PlayerSprites[0].color = color;

    }

    public void removeHelmet()
    {
        hasHelmet = false;
        PlayerSprites[3].sprite = localPlayerSprites[3];
        PlayerSprites[3].color = hairColor;
    }

    public void removeArmor()
    {
        PlayerSprites[2].sprite = localPlayerSprites[2];
        PlayerSprites[2].color = shirtColor;
    }

    public void removeShoes()
    {
        PlayerSprites[0].color = shoeColor;
    }
}
