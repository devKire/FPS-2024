using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponModel : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private float spread;
    [SerializeField] private float reloadTime;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private int magazineCap;
    [SerializeField] private int bulletsForShoot;
    [SerializeField] private bool automatic;
    [SerializeField] private bool scope;
    [SerializeField] private Mesh model;
    [SerializeField] private Material material;
    [SerializeField] private Vector3 weaponPosition;

    public float Damage => damage;
    public float Range => range;
    public float FireRate => fireRate;
    public float Spread => spread;
    public float ReloadTime => reloadTime;
    public float TimeBetweenShoots => timeBetweenShoots;
    public int MagazineCap => magazineCap;
    public int BulletsForShoot => bulletsForShoot;
    public bool Automatic => automatic;
    public bool Scope => scope;
    public Mesh Model => model;
    public Material Material => material;
    public Vector3 WeaponPosition => weaponPosition;
}
