/* 
 * This is the Achievement this contains just one achievement
 * but stores all of the properties needed to finish the achievement
 * These properties are call AchievementProperties and it stores it in 
 * a array.  When called upon to see if it has completed the achievement
 * it will search the array for all its propeties and set them first.
 */
using UnityEngine;
using System.Collections;
using System;

namespace BoogieDownGames {

	[Serializable]
	public class Achievement : PropertyAttribute {

		[SerializeField]
		private string m_name; //The name of the achievement

		[SerializeField]
		private AchievementProperties [] m_properties; //Array containing the achievements

		[SerializeField]
		private bool m_isComplete; //is the achievement complete

		public bool IsCompleted
		{
			get { return m_isComplete; }
			set { m_isComplete = true; }
		}

		//This checks to see if all of the properties have been unlocked
		public bool CheckForUnlock()
		{
			int count = 0;
			foreach(AchievementProperties prop in m_properties) {
				if(prop.IsUnLocked == true) {
					count++;
				}
			}

			if(count >= m_properties.Length) {
				m_isComplete = true;
			}
			//send a message to those that need to know if we accomblished the feat
			return m_isComplete;
		}

		public bool CheckProperty(string p_prop)
		{
			bool unlock = false;
			//Dont waste cycles if it is done already
			if(m_isComplete == false) {
				//search for the property
				foreach(AchievementProperties prop in m_properties) {

					if(prop.Name == p_prop) {
						//Increment the property
						prop.CurrentValue++;
						//ok now we found it get the opcode and set the property
						switch(prop.OpCode) {

						case Operator.EqualTo:
			
							if(prop.CurrentValue == prop.UnLockValue) {
								prop.IsUnLocked = true;
							}
							break;

						case Operator.GreaterThan:
							if(prop.CurrentValue > prop.UnLockValue) {
								prop.IsUnLocked = true;
							}
							break;
						case Operator.GreaterThanOrEqualTo:
							if(prop.CurrentValue >= prop.UnLockValue) {
								prop.IsUnLocked = true;
							}
							break;

						case Operator.LessThan:
							if(prop.CurrentValue < prop.UnLockValue) {
								prop.IsUnLocked = true;
							}
							break;

						case Operator.LesstThanOrEqualTo:
							if(prop.CurrentValue <= prop.UnLockValue) {
								prop.IsUnLocked = true;
							}
							break;
						}

						//Now check for unlock
						unlock = CheckForUnlock();
					}
				}
			}
			return unlock;
		}

	}
}