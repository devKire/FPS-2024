using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponModel weapon; // Referência ao ScriptableObject que contém os dados da arma
    [SerializeField] private Transform firePoint; // Ponto de onde os tiros são disparados
    [SerializeField] private GameObject bulletImpact; // Objeto de impacto dos projéteis

    private int currentMagazine; // Quantidade atual de balas no carregador
    private int reserveAmmo; // Quantidade de munição reserva
    private bool isFiring; // Flag para verificar se a arma está disparando
    private bool isReloading; // Flag para verificar se a arma está recarregando

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
        // Atualiza a aparência da arma com os dados do WeaponModel
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (meshFilter != null && meshRenderer != null)
        {
            meshFilter.mesh = weapon.Model;
            meshRenderer.material = weapon.Material;
        }

        // Inicialize o carregador atual com a capacidade máxima do carregador da arma
        currentMagazine = weapon.MagazineCap;
        reserveAmmo = 100; // Inicialize a munição reserva com um valor padrão
    }

    private IEnumerator FireCoroutine()
    {
        isFiring = true;

        while (currentMagazine > 0)
        {
            // Verifica se pode disparar
            if (currentMagazine > 0)
            {
                // Reduz a munição do carregador
                currentMagazine--;

                // Dispara projéteis
                for (int i = 0; i < weapon.BulletsForShoot; i++)
                {
                    Shoot();
                }

                // Define o tempo para o próximo disparo
                yield return new WaitForSeconds(weapon.TimeBetweenShoots);
            }
            else
            {
                // Se a munição acabar, interrompa o disparo
                break;
            }
        }

        isFiring = false;
    }

    private void Shoot()
    {
        // Implementação do método Shoot
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

        // Verifica se precisa recarregar e se há munição disponível
        int bulletsNeeded = weapon.MagazineCap - currentMagazine;
        int bulletsToLoad = Mathf.Min(bulletsNeeded, reserveAmmo);

        // Atualiza a munição no carregador e no inventário
        currentMagazine += bulletsToLoad;
        reserveAmmo -= bulletsToLoad;

        isReloading = false;
    }

    private void OnDrawGizmos()
    {
        // Você pode adicionar gizmos para visualizar coisas como o alcance do disparo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, weapon.Range);
    }
}
