using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuScript : MonoBehaviour
{
    public Image doubleAttack, heal, slide;
    public Text doubleAttack_cooldown,heal_cooldown, slide_cooldown;
    private float cooldown_f = 4, cooldown_c = 6, cooldown_q = 15;
    public bool canHeal = true, canSlide = true, canDoubleAttack = true;
    void Start()
    {
        
    }


    void Update()
    {
        if (!canDoubleAttack)
            UseDoubleAttack();
        if (!canHeal)
            UseHeal();
        if (!canSlide)
            UseSlide();
    }

    void UseDoubleAttack()
    {
        doubleAttack.color = new Color(doubleAttack.color.r, doubleAttack.color.g, doubleAttack.color.b, 0.5f);
        cooldown_f -= 1 * Time.smoothDeltaTime;
        doubleAttack_cooldown.enabled = true;
        doubleAttack_cooldown.text = cooldown_f.ToString("0");
        if(cooldown_f < 1)
        {
            doubleAttack.color = new Color(doubleAttack.color.r, doubleAttack.color.g, doubleAttack.color.b, 1f);
            doubleAttack_cooldown.enabled = false;
            cooldown_f = 4;
            canDoubleAttack = true;
        }
    }

    void UseHeal()
    {
        heal.color = new Color(heal.color.r, heal.color.g, heal.color.b, 0.5f);
        cooldown_q -= 1 * Time.smoothDeltaTime;
        heal_cooldown.enabled = true;
        heal_cooldown.text = cooldown_q.ToString("0");
        if(cooldown_q < 1)
        {
            heal.color = new Color(heal.color.r, heal.color.g, heal.color.b, 1f);
            heal_cooldown.enabled = false;
            cooldown_q = 15;
            canHeal = true;
        }
    }

    void UseSlide()
    {
        slide.color = new Color(slide.color.r, slide.color.g, slide.color.b, 0.5f);
        cooldown_c -= 1 * Time.smoothDeltaTime;
        slide_cooldown.enabled = true;
        slide_cooldown.text = cooldown_c.ToString("0");
        if(cooldown_c < 1)
        {
            slide.color = new Color(slide.color.r, slide.color.g, slide.color.b, 1f);
            slide_cooldown.enabled = false;
            cooldown_c = 6;
            canSlide = true;
        }
    }

}
