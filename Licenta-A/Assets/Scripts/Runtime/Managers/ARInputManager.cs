using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace AF
{
	public class ARInputManager : MonoBehaviour
	{
		internal struct NullablePose
		{
			internal Vector3? position;
			internal Quaternion? rotation;
		}

		static readonly internal List<XRNodeState> nodeStates = new List<XRNodeState>();

		private void Update()
		{
			var updatedPose = GetNodePoseData(XRNode.CenterEye);
			if (updatedPose.position.HasValue)
			{
				Debug.Log("Position: " + updatedPose.position.Value);
			}
			if (updatedPose.rotation.HasValue)
			{
				Debug.Log("Rotation: " + updatedPose.rotation.Value);
			}
		}
		static internal NullablePose GetNodePoseData(XRNode currentNode)
		{
			var resultPose = new NullablePose();
			InputTracking.GetNodeStates(nodeStates);
			foreach (var nodeState in nodeStates)
			{
				if (nodeState.nodeType == currentNode)
				{
					var pose = Pose.identity;
					var positionSuccess = nodeState.TryGetPosition(out pose.position);
					var rotationSuccess = nodeState.TryGetRotation(out pose.rotation);

					if (positionSuccess)
					{
						resultPose.position = pose.position;
					}

					if (rotationSuccess)
					{
						resultPose.rotation = pose.rotation;
					}

					return resultPose;
				}
			}
			return resultPose;
		}
	}
}
