using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IndividualPos : MonoBehaviour
{
    public enum Type {Pickaxe, Knife, Bomb}
    public Type weaponType;

    [SerializeField] IndividualPos pickaxe, knife, bomb;

    [SerializeField] float travelTime;
    [SerializeField] int i;

    public bool blocked;
    private void Update()
    {
        if (i == 0 && transform.position != SelectWeapon.pickaxePos)
        {
            transform.DOMove(SelectWeapon.pickaxePos, travelTime).SetEase(Ease.OutElastic);
        }
        else if (i == 1 && transform.position != SelectWeapon.knifePos)
        {
            transform.DOMove(SelectWeapon.knifePos, travelTime).SetEase(Ease.OutElastic);
        }
        else if (i == 2 && transform.position != SelectWeapon.bombPos)
        {
            transform.DOMove(SelectWeapon.bombPos, travelTime).SetEase(Ease.OutElastic);
        }
    }
    public void SelectLeft()
    {
        if (i >= 2)
        {
            i = 0;
        }
        else
        {

            switch (weaponType)
            {
                case Type.Pickaxe:
                    if (knife.blocked)
                    {
                        transform.DOShakePosition(1);
                        transform.DOShakeRotation(1);
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Type.Knife:
                    if (bomb.blocked)
                    {
                        transform.DOShakePosition(1);
                        transform.DOShakeRotation(1);
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case Type.Bomb:
                    if (pickaxe.blocked)
                    {
                        transform.DOShakePosition(1);
                        transform.DOShakeRotation(1);
                    }
                    else
                    {
                        i++;
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void SelectRight()
    {
        if (i <= 0)
        {
            i = 2;
        }
        else
        {
            

            switch (weaponType)
            {
                case Type.Pickaxe:
                    if (knife.blocked)
                    {
                        transform.DOShakePosition(1);
                        transform.DOShakeRotation(1);
                    }
                    else
                    {
                        i--;
                    }
                    break;
                case Type.Knife:
                    if (bomb.blocked)
                    {
                        transform.DOShakePosition(1);
                        transform.DOShakeRotation(1);
                    }
                    else
                    {
                        i--;
                    }
                    break;
                case Type.Bomb:
                    if (pickaxe.blocked)
                    {
                        transform.DOShakePosition(1);
                        transform.DOShakeRotation(1);
                    }
                    else
                    {
                        i--;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
