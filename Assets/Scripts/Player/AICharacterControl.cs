using System;
using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;

        [SerializeField]
        private UnityEngine.AI.NavMeshAgent agent;    

        public GameObject target;                                  
        public float animatorSpeed = 0.75f;

        private void Start()
        {
            this.playerController = this.GetComponent<PlayerController>();

            if (this.agent == null)
                agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        }

        public IEnumerator AgentMovementRoutine()
        {
            while (true)
            {
                agent.SetDestination(target.transform.position);
                this.playerController.animatorController.SetSpeed(1f);
                yield return new WaitForEndOfFrame();
            }
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
            this.playerController.TogglePlayerForAIMovement(true);
            agent.SetDestination(target.transform.position);
            StartCoroutine(this.AgentMovementRoutine());
        }

        public void OnTargetReach()
        {
            this.StopAllCoroutines();
            this.playerController.TogglePlayerForAIMovement(false);
        }

       
    }
}
