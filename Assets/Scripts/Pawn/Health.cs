using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;

    public UnityEvent OnHeal;
    public UnityEvent OnDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayHealthSound()
    {
        // TODO: Play Health sound.
    }

    public void PlayHealthParticle()
    {
        // TODO: Play Health particles.
    }

    public void Heal(float amountToHeal)
    {
        // Add amount to health.
        currentHealth += amountToHeal;
        // Don't go over max.
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        // Call all the functions connected to on heal.
        OnHeal.Invoke();
    }

    public void Hurt(float amountToHurt)
    {
        // subtract amount to health.
        currentHealth -= amountToHurt;
        // Don't go under 0.
        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }
        // Call all the functions connected to on heal.
        OnHeal.Invoke();
    }
}
