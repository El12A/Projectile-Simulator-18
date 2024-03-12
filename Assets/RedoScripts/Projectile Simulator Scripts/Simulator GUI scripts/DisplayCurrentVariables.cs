using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    // this script has two main functions
    // updating the current variables text found at the top right of the GUI screen
    // and handling the timestamp buttons
    public class DisplayCurrentVariables : PhysicsSimulator
    {
        // references to all the current text variables of the top right panel
        [SerializeField] private TMP_Text displacementText;
        [SerializeField] private TMP_Text velocityText;
        [SerializeField] private TMP_Text accelerationText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text rotationText;

        private float nextSecondToSave = 1;

        // variables for all the important current information on the projectile whilst it is in motion
        public float time;
        private Vector3 currentDisplacement;
        private Vector3 currentVelocity;
        private Vector3 currentAcceleration;
        private Vector3 currentRotation;
        private string lastButton;

        // two stacks of timestamps for going back or forward 1s in the projectiles journey (works like undo and redo buttons) 
        private Stack<TimeStamp> undoTimeStampStack;
        private Stack<TimeStamp> redoTimeStampStack;

        private void Start()
        {
            // on start get reference to projectile and initiate empty timstamp stacks with max of 20 but anyways they will expand capacity if they get full
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
            undoTimeStampStack = new Stack<TimeStamp>(20);

            redoTimeStampStack = new Stack<TimeStamp>(20);
        }
        // Update is called once per frame
        void Update()
        {
            // start updating the text for current variables only when the projectile is in motion;
            if (projectileMotion.inMotion == true)
            {
                time += Time.deltaTime;

                currentDisplacement = projectile.projectileRb.position - projectile.initialPosition;
                currentVelocity = projectile.projectileRb.GetPointVelocity(projectile.projectileRb.position);
                currentAcceleration = variableController.acceleration;
                currentRotation = projectile.projectileObject.transform.rotation.eulerAngles;

                UpdateCurrentVariablesText();
                // the time will never be precisely one as update is run at every fram
                // for this reason when another second has passed during update function then it will enter the if statement and save the timestamp to the stack
                if (time > nextSecondToSave)
                {
                    Debug.Log("save" + nextSecondToSave);
                    nextSecondToSave++;
                    AddTimeStampToStack();

                }
            }
            // when projectile is reset so is the text of the current variables and the stacks
            if (Input.GetKeyDown(KeyCode.R))
            {
                time = 0;
                timeText.text = "Time: ";
                velocityText.text = "Velocity: ";
                accelerationText.text = "Acceleration: ";
                displacementText.text = "Displacement: ";
                rotationText.text = "rotation: ";
                undoTimeStampStack.Clear();
                redoTimeStampStack.Clear();
            }
        }
        private void UpdateCurrentVariablesText()
        {
            // show displacement by getting string version of current position and taking it away from current rigidbody component position
            displacementText.text = "Displacement: " + (currentDisplacement).ToString();
            velocityText.text = "Velocity: " + (currentVelocity).ToString();
            // show acceleration that has been calculated from variableController as acceleration is constant
            accelerationText.text = "Acceleration: " + (currentAcceleration).ToString();
            //convert time to text
            //sometimes the number of sf of time is less than 4 in that case we can just no show the time for that one frame as unnoticable to the human eye anyways
            try
            {
                timeText.text = "Time: " + time.ToString().Substring(0, Mathf.Min(4, time.ToString().Length));
            }
            catch
            {
                timeText.text = "Time: ";
            }
            //takes the rotation of the projectileobject and converts it to string
            rotationText.text = "Rotation" + currentRotation.ToString();
        }

        // adds another timestamp to the stack 
        // called every second during projectiles motion
        private void AddTimeStampToStack()
        {
            TimeStamp newTimeStamp = new TimeStamp(time, currentVelocity, currentAcceleration, currentDisplacement, currentRotation);
            undoTimeStampStack.Push(newTimeStamp);
        }

        // called when back 1s time button is pressed will go back in time to last second and will move projectile there and will also update variables displayed for user
        public void GoBack1sTimeStamp()
        {
            Debug.Log("undo " + undoTimeStampStack.GetCount());
            Debug.Log("redo " + redoTimeStampStack.GetCount());
            // only call function if undoTimeStampStack has another item to pop
            if (undoTimeStampStack.GetCount() > 0)
            {
                UpdateTimeStamp(undoTimeStampStack, redoTimeStampStack);
                // if last call was the other button now execute function one more time to get it to go to next timestamp as currently has only loaded the same one just moved it onto the other stack
                if (lastButton == "redo")
                {
                    UpdateTimeStamp(redoTimeStampStack, undoTimeStampStack);
                    lastButton = "undo";
                }
            }
        }
        // called when forward 1s time button is pressed will go forwards in time to next second and will move projectile there and will also update variables displayed for user but will only go up to max the point the projectile has currently reached whilst moving.
        public void GoForwards1sTimeStamp()
        {
            Debug.Log("undo " + undoTimeStampStack.GetCount());
            Debug.Log("redo " + redoTimeStampStack.GetCount());
            // only call function if redoTimeStampStack has another item to pop
            if (redoTimeStampStack.GetCount() > 0)
            {
                UpdateTimeStamp(redoTimeStampStack, undoTimeStampStack);
                // if last call was the other button now execute function one more time to get it to go to next timestamp as currently has only loaded the same one just moved it onto the other stack
                if (lastButton == "undo")
                {
                    UpdateTimeStamp(redoTimeStampStack, undoTimeStampStack);
                    lastButton = "redo";
                }
            }
        }

        // if PastTimeStamp() is called
        // gets the top element in undoTimeStampStack and pops it and saves it temporarily in currentTimeStamp variable
        // then will udpate the currentvariables using the TimeStamp data
        // will then Update the variables text and finally push the timestamp of redoTimeStampStack stack so the user can come back to the timestamp (works as a redo button).
        // Or if NextTimeStamp() is called the opposite happens
        private void UpdateTimeStamp(Stack<TimeStamp> stackToPop, Stack<TimeStamp> stackToPush)
        {
            TimeStamp currentTimeStamp = stackToPop.Pop();
            currentDisplacement = currentTimeStamp.displacement;
            currentVelocity = currentTimeStamp.velocity;
            currentAcceleration = currentTimeStamp.acceleration;
            currentRotation = currentTimeStamp.rotation;
            time = currentTimeStamp.time;
            // move the projectile to the position in time specified by timestamp
            projectile.projectileObject.transform.position = projectile.initialPosition + currentDisplacement;
            UpdateCurrentVariablesText();
            stackToPush.Push(currentTimeStamp);
        }

        // this function pushes all the remaining timestamps inside of undoTimeStampStack onto undoTimeStampStack
        // this is called in the projectile motion script when the unpause button is pressed as we want the undo stack to be refilled
        public void ResetStack()
        {
            for (int i = 0; i < redoTimeStampStack.GetCount(); i++)
            {
                undoTimeStampStack.Push(redoTimeStampStack.Pop());
                Debug.Log(undoTimeStampStack.Peek());
            }
        }
    }
}
