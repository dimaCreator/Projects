using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")]
    public float waveFrequency = 2;//число секунд полного цикла синусоиды
    //ширина синусоиды в метрах
    public float waveWidth = 4;
    public float waveRoty = 45;

    private float x0;//начальное значение координаты X
    private float birthTime;

    //заметь, что start не используется классом Enemy
    void Start()
    {
        x0 = pos.x;

        birthTime = Time.time;
    }

    //переопределим метод Move класса Entmy для класса Enemy_1
    public override void Move()
    {
        // Так как pos - это свойство, нельзя напрямую изменить pos.x
        // поэтому получим pos в виде вектора Vector3, доступного для изменения
        Vector3 tempPos = pos;
        //И значение theta изменяется с течением времени
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        Vector3 rot = new Vector3(0, sin * waveRoty, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        // base.Move() обрабатывает движение вниз, вдоль оси Y
        base.Move();


        //Debug.Log(bndCheck.isOnScreen);
    }
}
