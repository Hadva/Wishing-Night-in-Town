﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class DisplayChoices : Command
    {    
        [SerializeField]
        private Choice[] m_Choices;

        private Status m_Status = Status.Continue;

        public override void Enter()
        {
            m_Status = Status.Continue;
            DisplayManager.instance.DisplayChoices(m_Choices);
            // hook on choice selected event
            DisplayManager.instance.onChoiceSelected = SelectChoice;
        }

        protected override Status UpdateNode()
        {     
            return m_Status;
        }

        /// <summary>
        /// Continue status
        /// </summary>
        private void SelectChoice(int index)
        {
            m_Choices[index].decisionCommand.Enter();
            m_Choices[index].decisionCommand.Execute();
            if (m_Choices[index].decisionCommand is DisplayLine)
            {
                DisplayManager.instance.onNextLine = Continue;
            }
            else
            {
                m_Status = Status.Success;
            }
        }

        private void Continue()
        {
            m_Status = Status.Success;
        }

    }
}
