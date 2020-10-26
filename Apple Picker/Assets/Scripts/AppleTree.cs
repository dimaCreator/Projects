using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    /// <summary>
    /// если поставить галочку isKinematic то обьект не будет менять местоположение от столкновений 
    /// и его передвежением можно будет управлятьт только из кода
    /// 
    /// есть понятие физические слои и их можно настроить так, чтобы некоторые слои физически не взаимодействовали с другими
    /// Edit > Project Settings > Physics
    /// </summary>
    [Header("Set in Inspector")]
    public GameObject applePrefab;
    public float speed;
    public float leftAndRightEdge = 10f;// расстояние, на котором должно изменяться направление движения яблони
    public float chanceToChangeDirections = 0.1f;//вероятность случайного изменения движения
    public float secondsBetweenAppleDrops = 1f;//частота создания яблока

    private void Start()
    {
        Invoke("DropApple", 2f);//вызывает DropApple через 2 секунды
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    private void Update()// update может вызываться до 400 раз в секунду
    {
        Vector3 pos = transform.position;
        //Random.value возвращает float от 0 до 1
        if (Mathf.Abs(pos.x) > leftAndRightEdge)// может быть стоит оптимизировать
        {
            speed *= -1;
        }
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
    }

    private void FixedUpdate()//вызывается точно 50 раз в секунду
    {
        if(Random.value < chanceToChangeDirections)
        {
            speed *= -1;
        }
    }

}
