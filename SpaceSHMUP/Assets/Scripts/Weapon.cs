using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    none, // по умолчанию / нет оружия
    blaster, // Простой бластер
    spread,  // Веерная пушка, стреляющая несколькими снарядами
    phaser, // Волновой фазер(дореализовывать)
    missile, // Самонаводящиеся ракеты(дореализовывать)
    laser, // Наносит повреждения при долговременном воздействии(дореализовывать)
    shield // Увеличивает shieldLevel
}

public class Weapon : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
