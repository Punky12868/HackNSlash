using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class IndividualPos : MonoBehaviour
{
    public enum Type {Pickaxe, Knife, Bomb}
    public Type weaponType;

    [SerializeField] int damage;

    [SerializeField] IndividualPos pickaxe, knife, bomb;

    [SerializeField] float travelTime;
    public int i;
    int index;

    public bool blocked;

    [SerializeField] float cooldown;
    float internalCooldown;
    bool cantChange;

    Vector3 storedTransformPos, storedTransformRot;
    private void Awake()
    {
        index = i;
        storedTransformPos = transform.position;
        storedTransformRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        switch (weaponType)
        {
            case Type.Pickaxe:
                Weapon.pickaxe = this;
                Weapon.pickDamage = damage;
                break;
            case Type.Knife:
                Weapon.knife = this;
                Weapon.knifeDamage = damage;
                break;
            case Type.Bomb:
                Weapon.bomb = this;
                Weapon.bombDamage = damage;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (blocked && i != index)
        {
            if (index == 0)
            {
                bomb.i = bomb.index;
                knife.i = knife.index;
            }
            else if (index == 1)
            {
                bomb.i = bomb.index;
                pickaxe.i = pickaxe.index;
            }
            else if (index == 2)
            {
                pickaxe.i = pickaxe.index;
                knife.i = knife.index;
            }

            i = index;
        }

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

        if (internalCooldown > 0)
        {
            internalCooldown -= Time.deltaTime;
            cantChange = true;
        }
        else
        {
            cantChange = false;

            /*if (transform.position != storedTransformPos)
            {
                transform.DOMove(storedTransformPos, .5f);
            }*/

            if (new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z) != storedTransformRot)
            {
                transform.DORotate(storedTransformRot, .5f);
            }
        }
    }
    public void SelectLeft()
    {
        if (!blocked && !cantChange)
        {
            internalCooldown = cooldown;
            switch (weaponType)
            {
                case Type.Pickaxe:
                    PickaxeType(false);
                    break;
                case Type.Knife:
                    KnifeType(false);
                    break;
                case Type.Bomb:
                    BombType(false);
                    break;
                default:
                    break;
            }
        }
    }
    public void SelectRight()
    {
        if (!blocked && !cantChange)
        {
            internalCooldown = cooldown;
            switch (weaponType)
            {
                case Type.Pickaxe:
                    PickaxeType(true);
                    break;
                case Type.Knife:
                    KnifeType(true);
                    break;
                case Type.Bomb:
                    BombType(true);
                    break;
                default:
                    break;
            }
        }
    }
    void Select(bool b)
    {
        if (b)
        {
            if (i >= 2)
            {
                i = 0;
            }
            else
            {
                i++;
            }
        }
        else
        {
            if (i <= 0)
            {
                i = 2;
            }
            else
            {
                i--;
            }
        }
    }
    void PickaxeType(bool b)
    {
        if (bomb.blocked && knife.blocked)
        {
            transform.DOShakePosition(.5f);
            transform.DOShakeRotation(.5f);
        }
        else if (!bomb.blocked && knife.blocked)
        {
            
            if (i == bomb.index)
            {
                i = index;
            }
            else
            {
                i = bomb.index;
            }
        }
        else if (bomb.blocked && !knife.blocked)
        {
            if (i == knife.index)
            {
                i = index;
            }
            else
            {
                i = knife.index;
            }
        }
        else
        {
            if (b)
            {
                Select(true);
            }
            else
            {
                Select(false);
            }
        }
    }
    void KnifeType(bool b)
    {
        if (bomb.blocked && pickaxe.blocked)
        {
            transform.DOShakePosition(.5f);
            transform.DOShakeRotation(.5f);
        }
        else if (!bomb.blocked && pickaxe.blocked)
        {
            if (i == bomb.index)
            {
                i = index;
            }
            else
            {
                i = bomb.index;
            }
        }
        else if (bomb.blocked && !pickaxe.blocked)
        {
            if (i == pickaxe.index)
            {
                i = index;
            }
            else
            {
                i = pickaxe.index;
            }
        }
        else
        {
            if (b)
            {
                Select(true);
            }
            else
            {
                Select(false);
            }
        }
    }
    void BombType(bool b)
    {
        if (bomb.blocked && knife.blocked)
        {
            transform.DOShakePosition(.5f);
            transform.DOShakeRotation(.5f);
        }
        else if (!pickaxe.blocked && knife.blocked)
        {
            if (i == pickaxe.index)
            {
                i = index;
            }
            else
            {
                i = pickaxe.index;
            }
        }
        else if (pickaxe.blocked && !knife.blocked)
        {
            if (i == knife.index)
            {
                i = index;
            }
            else
            {
                i = knife.index;
            }
        }
        else
        {
            if (b)
            {
                Select(true);
            }
            else
            {
                Select(false);
            }
        }
    }

    public void LockUnlock(bool b)
    {
        if (b)
        {
            blocked = true;
        }
        else
        {
            blocked = false;
        }
    }
}
