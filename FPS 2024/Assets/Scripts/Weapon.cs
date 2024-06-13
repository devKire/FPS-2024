using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponModel weapon; // Refer�ncia ao ScriptableObject que cont�m os dados da arma
    [SerializeField] private Transform firePoint; // Ponto de onde os tiros s�o disparados
    [SerializeField] private GameObject bulletImpact; // Objeto de impacto dos proj�teis

    private int currentMagazine; // Quantidade atual de balas no carregador
    private int reserveAmmo; // Quantidade de muni��o reserva
    private bool isFiring; // Flag para verificar se a arma est� disparando
    private bool isReloading; // Flag para verificar se a arma est� recarregando

    private void Start()
    {
        UpdateWeapon();
    }

    private void Update()
    {
        // Exemplo de input para disparar a arma
        if (Input.GetButtonDown("Fire1") && !isFiring && !isReloading)
        {
            StartCoroutine(FireCoroutine());
        }

        // Exemplo de input para recarregar a arma
        if (Input.GetKeyDown(KeyCode.R) && !isFiring && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void UpdateWeapon()
    {
        // Atualiza a apar�ncia da arma com os dados do WeaponModel
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (meshFilter != null && meshRenderer != null)
        {
            meshFilter.mesh = weapon.Model;
            meshRenderer.material = weapon.Material;
        }

        // Inicialize o carregador atual com a capacidade m�xima do carregador da arma
        currentMagazine = weapon.MagazineCap;
        reserveAmmo = 100; // Inicialize a muni��o reserva com um valor padr�o
    }

    private IEnumerator FireCoroutine()
    {
        isFiring = true;

        while (currentMagazine > 0)
        {
            // Verifica se pode disparar
            if (currentMagazine > 0)
            {
                // Reduz a muni��o do carregador
                currentMagazine--;

                // Dispara proj�teis
                for (int i = 0; i < weapon.BulletsForShoot; i++)
                {
                    Shoot();
                }

                // Define o tempo para o pr�ximo disparo
                yield return new WaitForSeconds(weapon.TimeBetweenShoots);
            }
            else
            {
                // Se a muni��o acabar, interrompa o disparo
                break;
            }
        }

        isFiring = false;
    }

    private void Shoot()
    {
        // Implementa��o do m�todo Shoot
    }

    private void Reload()
    {
        if (!isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        // Aguarda o tempo de recarga
        yield return new WaitForSeconds(weapon.ReloadTime);

        // Verifica se precisa recarregar e se h� muni��o dispon�vel
        int bulletsNeeded = weapon.MagazineCap - currentMagazine;
        int bulletsToLoad = Mathf.Min(bulletsNeeded, reserveAmmo);

        // Atualiza a muni��o no carregador e no invent�rio
        currentMagazine += bulletsToLoad;
        reserveAmmo -= bulletsToLoad;

        isReloading = false;
    }

    private void OnDrawGizmos()
    {
        // Voc� pode adicionar gizmos para visualizar coisas como o alcance do disparo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, weapon.Range);
    }
}
