using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    [HideInInspector]
    public Character character;

    public Image meterImage;

    public Image armorMeterImage;
    public Text armorText;

    public Text hpText;

    public GameObject[] armors;

    float maxHitPoints;

    void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }

    void Update()
    {
        if (character != null)
        {
            armorMeterImage.fillAmount = character.GetComponent<Player>().amor;
            meterImage.fillAmount = hitPoints.value / maxHitPoints;
            hpText.text = "HP:" + (int)(hitPoints.value * 10);
            armorText.text = (int)(armorMeterImage.fillAmount * 100) + "%";
        }
    }

    public void ActivateArmor(int index, Color color)
    {
        armors[index].SetActive(true);
        armors[index].GetComponent<Image>().color = color;

    }
}
