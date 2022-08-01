/*
 * @author Marty Fitzer
 */

using UnityEngine;

namespace KonnectVR
{
    public static class JointExtensions
    {
        /// <summary>
        /// Transfers a joint with all its settings from one GameObject to another.
        /// </summary>
        /// <param name="joint">The joint being transferred.</param>
        /// <param name="transferTarget">The GameObject the joint is being transferred to.</param>
        /// <returns>The transferred joint.</returns>
        public static Joint transferJoint(this Joint joint, GameObject transferTarget)
        {
            Joint transferredJoint = transferTarget.AddComponent(joint);
            transferredJoint.connectedAnchor = Vector3.zero; //This value gets messed with when transferring joints

            //Destroy original joint
            GameObject.Destroy(joint);

            return transferredJoint;
        }
    }
}
