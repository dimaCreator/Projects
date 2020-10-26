using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;


    [Header("Set Dynamicaly")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingmode;

    static private Slingshot S;
    private Rigidbody projectileRigidbody;
    static public Vector3 LAUNCH_POS
    {
        get
        {
            if(S == null)
            {
                return Vector3.zero;
            }
            return S.launchPos;
        }
    }

    private void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");//найдет дочерний обьект с именем LaunchPoint
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);// нельзя влиять на halo непосредственно, поэтому приходиться искать обходное решение
        launchPos = launchPointTrans.position;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Slingshot:OnMousEnter");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (true)
        {
            Debug.Log("Slingshot:OnMousExit");
            launchPoint.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        aimingmode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (!aimingmode) return;

        //получаем текущие экранные координаты указателя мыши
        Vector3 mousePos2D = Input.mousePosition;
        
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Debug.Log($"{mousePos2D}");
        Vector3 mouseDelta = mousePos3D - launchPos;

        //ограничиваем mauseDelta радиусом коллайдера
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();//сохраняет направление, но делает единичным
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
            aimingmode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;// Эта операция не удалит созданный экземпляр Projectile, но освободит поле projectile для записи в него ссылки на другой экземпляр
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }
    }
}
