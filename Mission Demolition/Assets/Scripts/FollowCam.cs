using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;// ссылка на интересующий обьект point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamicaly")]
    public float camZ;
    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()// снарядом управляет физический движок PhysX, а обновление физики происходит с вызовами метода FixedUpdate().
    {

        Vector3 destination;

        if(POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;
            if(POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }

        //ограничим X и Y минимальными значениями
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        //Определим точку между местоположением камеры и destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //выполняет интерполяцию между двумя точками, возвращая взвешенное среднее.Если полю easing присвоить 0, Lerp() вернет первую
        //точку(transform, position); если полю easing присвоить 1, Lerp() вернет вторую точку(destination).

       destination.z = camZ;
       transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }
}
