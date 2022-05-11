using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDig : MonoBehaviour
{
    public int fuelCost;
    public float delayAmount = 1;
    public Animator animator;

    public TileManager tileManager;
    public GameObject tilemap;
    
    public List<GameObject> DigOptions;
    private BoxCollider2D boxCollider2d;
    private PlayerController PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        PlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!isGrounded()){
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)){
            characterDig("down");
        }
        if (Input.GetKey(KeyCode.A)){
            characterDig("left");
        }
        if (Input.GetKey(KeyCode.D)){
            characterDig("right");
        }
    }


    public void addDigOption(GameObject option){
        DigOptions.Add(option);
    }

    public void removeDigOption(GameObject option){
        DigOptions.Remove(option);
    }

    private GameObject GetClosestDig(List<GameObject> diggable){
        GameObject dMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject d in diggable)
        {
            float dist = Vector3.Distance(d.transform.position, currentPos);
            if (dist < minDist)
            {
                dMin = d;
                minDist = dist;
            }
        }
        return dMin;
    }

    /**
    * Based on an input direction, check valid dig spots around the player.
    * If an available dig spot is in the same direction as the dig direction, destroy it
    **/
    private void characterDig(string inputDirection){

        List<GameObject> correctDirection = new List<GameObject>();

        foreach (GameObject diggable in DigOptions){
            Vector3 diggableDirection = gameObject.transform.InverseTransformPoint(diggable.transform.position);
            if (diggableDirection.y > 0.5){
                continue;
            }
            if (diggableDirection.y < 0){
                if(inputDirection != "down"){
                    continue;
                }
                correctDirection.Add(diggable);
                continue;
            }
            if (inputDirection == "left" && diggableDirection.x < 0){
                correctDirection.Add(diggable);
                continue;
            }
            if (inputDirection == "right" && diggableDirection.x > 0){
                correctDirection.Add(diggable);
                continue;
            }
        }

        if(correctDirection.Count == 0){
            return;
        }
        
        GameObject closestDigOption = GetClosestDig(correctDirection);

        StartCoroutine(Dig(closestDigOption, inputDirection));
    }

    public bool isGrounded(){
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeight);
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight));
        return raycastHit.collider != null;
    }

    //takes one diggable game object and goes through the entire dig procedure
    private IEnumerator Dig(GameObject diggable, string inputDirection){

        //disable actions before digging starts
        DigOptions.Remove(diggable);
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<CharacterDig>().enabled = false;

        //figure out which direction to face
        if(inputDirection == "down"){
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
    
            //adjust positioning if digging down
            Vector3 newPos = new Vector3(diggable.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z );
            LeanTween.move(gameObject, newPos, 0.1f).setEase(LeanTweenType.easeInSine);
        }
        if(inputDirection == "right"){
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);
        }
        if(inputDirection == "left"){
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
        }

        //play cracking animation
        diggable.GetComponent<Destructable>().startCracking();
        
        //play digging animation
        animator.SetBool("Digging", true);

        //play digging sound
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Drill");

        //check if more tiles need to be spawned
        tileManager.spawnMoreTiles();

        //wait for delay amount
        yield return new WaitForSeconds(delayAmount);

        //destroy block
        Destroy(diggable);

        //adjust tile map composite collider
        tilemap.GetComponent<CompositeCollider2D>().GenerateGeometry();

        //stop animations and sounds
        animator.SetBool("Digging", false);
        audioManager.Stop("Drill");

        //use fuel
        PlayerController.useFuel(fuelCost);

        //update score
        PlayerController.updateDepthScore();

        //re-enable player actions
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<CharacterDig>().enabled = true;
    }
}
