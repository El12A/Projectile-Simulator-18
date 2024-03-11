using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// First watched these two videos to understand the theory and how it is applied in computer graphics: https://www.youtube.com/watch?v=zjMuIxRvygQ and https://www.youtube.com/watch?v=Do_vEjd6gF0
// unity documentation on 4 x 4 rotations which are used for doing 3d rotations: https://docs.unity3d.com/ScriptReference/Matrix4x4.Rotate.html
// unity documentation on quaternion euler angles to understand how to use them in my object 3d rotation: https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html

namespace PhysicsProjectileSimulator
{
    public class AngularRotationInputController : PhysicsSimulator
    {

        //rotation Input Floats
        float rotateInX;
        float rotateInY;
        float rotateInZ;
        float rotationalSpeed;

        private bool applyRotation;

        [SerializeField] private TMP_InputField inputRotationX;
        [SerializeField] private TMP_InputField inputRotationY;
        [SerializeField] private TMP_InputField inputRotationZ;
        [SerializeField] private TMP_InputField inputRotationalSpeed;
        [SerializeField] private Toggle applyRotationToggle;

        [SerializeField] private GameObject rotationalAnglesGameobject;
        [SerializeField] private GameObject rotationalSpeedGameobject;
        // Start is called before the first frame update
        void Start()
        {
            ChangeApplyRotation();
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
        }

        // Update is called once per frame
        void Update()
        {
            // if the projectile is in motion and angular rotation toggle is set to on then apply the rotation matrix
            if (applyRotation == true && projectileMotion.inMotion == true)
            {
                ApplyRotationMatrix();
            }

        }

        private void ApplyRotationMatrix()
        {
            // setting rotation floats to the input text of the rotation input fields in the GUI
            rotateInX = float.Parse(inputRotationX.text);
            rotateInY = float.Parse(inputRotationY.text);
            rotateInZ = float.Parse(inputRotationZ.text);
            // setting rotation speed to that inputted by user in GUI
            rotationalSpeed = float.Parse(inputRotationalSpeed.text);

            // first we construct a new rotation matrix using the user inputs
            // we construct the matrix using quaternion euler
            // quaternion.Euler is used to create a quaternion that represents a rotation using euler angles and is required as the user inputs the euler angles and therefore they need to be converted to a rotational matrix
            Matrix4x4 rotationInputMatrix = Matrix4x4.Rotate(
                Quaternion.Euler(
                    rotateInX * rotationalSpeed * Time.deltaTime,
                    rotateInY * rotationalSpeed * Time.deltaTime,
                    rotateInZ * rotationalSpeed * Time.deltaTime)

                    );
            // now we want to get our current rotation of the projectile as we want to maintain its rotation but simply add this new user inputted rotation
            // first we create this new rotation 4 x 4 matrix
            // the Matrix4x4.TRS is used to make the matrix 4x4 but needs specification for the translate rotate and scale factors of the matrix
            // seeing as we want no change in translation we say vector3.zero, no change in scale we say Vector3.one to keep scale constant. For rotation we simply get the current rotation of the rigidbody.
            Matrix4x4 currentRotationMatrix = Matrix4x4.TRS(Vector3.zero, projectile.projectileRb.rotation, Vector3.one);

            //now we make a new matrix by combining the 4x4 matrices
            Matrix4x4 newRotationMatrixToApply = rotationInputMatrix * currentRotationMatrix;

            // now we convert the rotation matrix to a quaternion so that it can be applied to the rigidbody
            Quaternion newRotation = ConvertMatrixToQuaternion(newRotationMatrixToApply);
            // no we apply the new rotation quaternion to the rigidbody component of the projectile
            projectile.projectileRb.MoveRotation(newRotation);
        }

        //used to convert a matrix to a quaternion, this is required for applying rotations to rigidbody components
        private Quaternion ConvertMatrixToQuaternion(Matrix4x4 matrixToConvert)
        {
            // wasn't sure on how to convert the matrix to quaternion so i looked online and found a unity forum talking about it and used the code found there: https://forum.unity.com/threads/is-it-possible-to-get-a-quaternion-from-a-matrix4x4.142325/
            Quaternion newRotationQuaternion = Quaternion.LookRotation(matrixToConvert.GetColumn(2), matrixToConvert.GetColumn(1));
            return newRotationQuaternion;
        }

        // this method is called whenever the toggle buttton state is changed and it deactivates or activates the input fields for the angular rotation
        public void ChangeApplyRotation()
        {
            applyRotation = applyRotationToggle.isOn;
            rotationalAnglesGameobject.SetActive(applyRotation);
            rotationalSpeedGameobject.SetActive(applyRotation);
        }
    }
}

