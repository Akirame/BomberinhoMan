using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TypeOfPower
{
    MoreBombs,
    MoreFire,
    MoreHealth,
}
public class PowerUp : MonoBehaviour
{
    public int typeOfIndex = 0;
    private float LifeTime;
    private float timeRotat;
    private float maxRotat;
    private float minRotat;
    private TypeOfPower typePower;
    	
	void Start ()
    {
        LifeTime = 0;
        timeRotat = 0;        
        maxRotat = 180 + 20;
        minRotat = 180 - 20;   
    }
		
	void Update ()
    {
        if (LifeTime > 10)
            Destroy(this.gameObject);
        else
            LifeTime += Time.deltaTime;
        SmoothRotationZ();
        IndexToEnum();
    }
    /// <summary>
    /// rotacion suave en Z
    /// </summary>
    private void SmoothRotationZ()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90, transform.rotation.y, Mathf.Lerp(maxRotat, minRotat, timeRotat)));
        if (timeRotat >= 1)
        {
            float temp = maxRotat;
            maxRotat = minRotat;
            minRotat = temp;
            timeRotat = 0;
        }
        else
            timeRotat += Time.deltaTime;
    }
    /// <summary>
    /// Pasar del número a enumerador
    /// </summary>
    private void IndexToEnum()
    {
        switch (typeOfIndex)
        {
            case 0:
                typePower = TypeOfPower.MoreBombs;
                break;
            case 1:
                typePower = TypeOfPower.MoreFire;
                break;
            case 2:
                typePower = TypeOfPower.MoreHealth;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {            
            switch (typePower)
            {
                case TypeOfPower.MoreBombs:
                    Game.Get().BombCantPlusPlus();                    
                    Destroy(this.gameObject);
                    break;
                case TypeOfPower.MoreFire:
                    Game.Get().BombRangePlusPLus();                    
                    Destroy(this.gameObject);
                    break;
                case TypeOfPower.MoreHealth:
                    Game.Get().HealthPlusPLus();                    
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
