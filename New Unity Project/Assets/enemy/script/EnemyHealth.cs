using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    
    public Slider HealthBar;
    public TextMeshProUGUI hp;
    public GameObject popup;
    public float CurrentHealth;
    public Animator anim;

    void start()
    {
        hp = GetComponent<TextMeshProUGUI>();
        CurrentHealth = MaxHealth ;
        hp.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        anim.SetTrigger("hurt");
        HealthBar.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
   
          Destroy(gameObject);
        }
        hp.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();
        GameObject  popupclone = Instantiate(popup, transform.position, Quaternion.identity,transform);
        popupclone.GetComponent<TextMeshPro>().text = damage.ToString();

    }

    void Update()
    {
       // print(CurrentHealth);

    }

}