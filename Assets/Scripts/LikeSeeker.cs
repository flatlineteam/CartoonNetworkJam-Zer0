using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class LikeSeeker : MonoBehaviour 
    {    
        public Transform target;
        public float turnSpeedInDegrees = 1.0f;
        public float movementSpeed = 0.5f;
        public float hitRange = 0.5f;
        public float timeToSelfDestruct = 1.0f;

        private Vector2 direction;
        private float lifeTime;

    	// Use this for initialization
    	void Start () 
        {
            if (target == null)
                Destroy(this);

            if(direction == Vector2.zero)
                direction = ((Vector2)Random.onUnitSphere).normalized;
            turnSpeedInDegrees *= Random.Range(0.9f, 1.1f);
            movementSpeed *= Random.Range(0.8f, 1.2f);
    	}
    	
    	// Update is called once per frame
    	private void FixedUpdate () 
        {                 
            lifeTime += Time.fixedDeltaTime;
            var dirToTarget = target.position - transform.position;
            if(dirToTarget.magnitude <= hitRange) TargetHit();
            if(lifeTime >= timeToSelfDestruct) TargetHit();
            var angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
            var currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Debug.Log(currentAngle - angle);
            if((currentAngle - angle) + 180 >= 0.0f)
                currentAngle = currentAngle - turnSpeedInDegrees;            
            else
                currentAngle = currentAngle + turnSpeedInDegrees;

            direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * currentAngle), Mathf.Sin(Mathf.Deg2Rad * currentAngle));

            transform.Translate(-direction * movementSpeed);
    	}

        private void TargetHit()
        {
            LikeCounterController.Current.AddToScore(1);
            Destroy(gameObject);
        }
    }
}