/* 
 * this stores an achivement property
 * this provides a very flexible way of creating 
 * properties.  So instead of creating a new class with different
 * properties we have one class with an array of different properties
 */
using UnityEngine;
using System.Collections;
using System;
namespace BoogieDownGames{

	//Opcodes so that we have greater flexible comparisons
	public enum Operator {LessThan, EqualTo, GreaterThan, LesstThanOrEqualTo, GreaterThanOrEqualTo }

	[Serializable]
	public class AchievementProperties : PropertyAttribute {

		[SerializeField]
		private string m_name;

		[SerializeField]
		private string m_description;

		[SerializeField]
		private int m_currentValue;

		[SerializeField]
		private int m_unLockValue;

		[SerializeField]
		private int m_initialValue;

		[SerializeField]
		private bool m_isUnLocked;

		[SerializeField]
		private Operator m_opcode;

		#region PROPERTIES

		public string Name
		{
			get { return m_name; }
		}

		public string Description
		{
			get { return m_description; }
		}

		public int CurrentValue
		{
			get { return m_currentValue; }
			set { m_currentValue = value; }
		}

		public int UnLockValue
		{
			get { return m_unLockValue; }
			set { m_unLockValue = value; }
		}

		public int InitialValue
		{
			get { return m_initialValue; }
		}

		public Operator OpCode
		{
			get { return m_opcode; }
		}

		public bool IsUnLocked
		{
			get { return m_isUnLocked; }
			set { m_isUnLocked = value; }
		}

		#endregion
	}
}