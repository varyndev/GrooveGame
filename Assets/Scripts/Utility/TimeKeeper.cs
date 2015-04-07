/* Sometimes corroutines are not appropiate for the situations
 * So I made this simple timer to get stuff done in a more agile way.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BoogieDownGames {
	[Serializable]
	public class TimeKeeper : PropertyAttribute {
		
		[SerializeField]
		private float m_timeLimit;
		
		[SerializeField]
		private float m_counter;
		
		[SerializeField]
		private bool m_isDone;
		
		[SerializeField]
		private bool m_isLoop;
		
		enum LOOPMODE { ONCE, LOOP };
		[SerializeField]
		private LOOPMODE m_loopMode = LOOPMODE.ONCE;
		
		enum COUNTMODE { UP, DOWN };
		[SerializeField]
		private COUNTMODE m_countMode = COUNTMODE.DOWN;
		
		//Some delagates so we don't have to use if or switch
		delegate void LoopDelegate();
		LoopDelegate m_loopDeletate;
		
		delegate void CountModeDelegate();
		CountModeDelegate m_countModeDelegate;
		
		public bool m_trigger;
		
		public float TimeLimit
		{
			get { return m_timeLimit; }
			set { m_timeLimit = value; }
		}
		
		public float Counter
		{
			get { return m_counter; }
			set { m_counter = value; }
		}
		
		public bool IsDone
		{
			get { return m_isDone; }
			set { m_isDone = value;  }
		}
		
		public int LoopType
		{
			get { return (int)m_loopMode; }
		}
		
		public void run()
		{
			m_countModeDelegate();
		}
		
		void runTimerDown()
		{
			if(m_counter > 0) {
				m_counter -= Time.deltaTime;
			} else {
				m_isDone = true;
				m_trigger = true;
				if(m_loopMode == LOOPMODE.ONCE) {
					m_isDone = true;
				} else {
					m_isDone = false;
					m_counter = m_timeLimit;
				}
			}
		}
		
		void runTimerUp()
		{
			if(m_counter < m_timeLimit) {
				m_counter += Time.deltaTime;
			} else {
				m_isDone = true;
				m_trigger = true;
				if(m_loopMode == LOOPMODE.ONCE) {
					m_isDone = true;
				} else {
					m_isDone = false;
					m_counter = 0;
				}
			}
		}
		
		//Resets and starts the clock
		public void startClock()
		{
			switch(m_countMode) {
			case COUNTMODE.UP:
				m_counter = 0;
				m_countModeDelegate = runTimerUp;
				break;
			case COUNTMODE.DOWN:
				m_counter = m_timeLimit;
				m_countModeDelegate = runTimerDown;
				break;
			}
		}
	}
}