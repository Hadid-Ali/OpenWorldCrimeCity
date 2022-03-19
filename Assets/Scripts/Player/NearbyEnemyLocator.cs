using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyEnemyLocator : MonoBehaviour
{
    public CharacterController parentComponent;
    public List<CharacterController> listOfCharacters = new List<CharacterController>();

    private void Start()
    {
        this.parentComponent = this.GetComponentInParent<CharacterController>();
        this.listOfCharacters = new List<CharacterController>();
    }

    public bool isCharacterInList(CharacterController c)
    {
        return this.listOfCharacters.Contains(c);
    }

    public void CheckForTargetElimination(CharacterController c)
    {
        if (this.parentComponent.nearByEnemy)
        {
            if (this.parentComponent.nearByEnemy.Equals(c.gameObject))
            {
                if (this.parentComponent.nearByEnemy.GetComponent<CharacterController>())
                    this.parentComponent.nearByEnemy.GetComponent<CharacterController>().OnCharacterKilled -= this.EliminateTarget;
                this.EliminateTarget();
            }
        }
    }

    public void EliminateTarget()
    {
        this.listOfCharacters.Remove(this.parentComponent.nearByEnemy.GetComponent<CharacterController>());
        this.parentComponent.nearByEnemy = null;
        this.GetNextTarget();
    }

    public void GetNextTarget()
    {
        if(this.listOfCharacters.Count>0)
        {
            this.parentComponent.nearByEnemy = this.listOfCharacters[0].gameObject;

            if(this.parentComponent.nearByEnemy.GetComponent<CharacterController>())
                this.parentComponent.nearByEnemy.GetComponent<CharacterController>().OnCharacterKilled += this.EliminateTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CharacterController>())
        {
            CharacterController c = other.gameObject.GetComponent<CharacterController>();
            if (!this.isCharacterInList(c) & !c.isPlayer)
            {
                this.listOfCharacters.Add(c);

                if (!this.parentComponent.nearByEnemy)
                    this.GetNextTarget();

            }
        }
    }    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            CharacterController c = other.gameObject.GetComponent<CharacterController>();
            if (this.isCharacterInList(c))
            {
             //   this.listOfCharacters.Remove(c);
                this.CheckForTargetElimination(c);
            }
        }
    }
}
