using UnityEngine;
using System.Collections;
using Photon.Pun;
using System.IO;

namespace Com.MyCompany.MyGame
{
	public class PlayerAnimatorManager : MonoBehaviourPun
	{
		#region Private Fields

		[SerializeField]
		private float directionDampTime = 0.25f;
		Animator animator;

		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
			animator = GetComponent<Animator>();
		}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// </summary>
		void Update()
		{

			// Prevent control is connected to Photon and represent the localPlayer
			if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
			{
				return;
			}

			// failSafe is missing Animator component on GameObject
			if (!animator)
			{
				return;
			}

			// deal with Jumping
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

			// only allow jumping if we are running.
			if (stateInfo.IsName("Base Layer.Run"))
			{
				//When using trigger parameter
                if (Input.GetKeyDown(KeyCode.Space)) animator.SetTrigger("Jump");
			}

			if (Input.GetKeyDown(KeyCode.Mouse0)) animator.SetTrigger("Punch");


			// deal with movement
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			// prevent negative Speed.
			if (v < 0)
			{
				v = 0;
			}

			// set the Animator Parameters
			animator.SetFloat("Speed", h * h + v * v);
			animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
		}

		#endregion

	}
}