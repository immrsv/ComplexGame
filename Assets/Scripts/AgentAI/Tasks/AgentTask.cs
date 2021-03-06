﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AgentAI.Actions;

namespace AgentAI.Tasks {
    public abstract class AgentTask : MonoBehaviour {

        public uint TaskPool;

        public float MaxPriority = 1.0f;

        protected Queue<AgentAction> Actions = new Queue<AgentAction>();
        protected float stageBegan;

        public AgentAction CurrentAction { get { return (Actions.Count > 0) ? Actions.Peek() : null; } }

        public abstract float Priority { get; }
        //public virtual bool CanExit { get { return true; } }
        public abstract bool CanExit { get; }

        public abstract void Enter();
        public abstract void Exit();

        public virtual void UpdateTask() {
            if (Actions.Count == 0) return; // No More Actions

            if (CurrentAction.IsComplete) // If Action is complete, pop from queue
            {
                //Debug.Log("Ship [" + gameObject.name + "] doing [" + GetType().Name + "] completed action [" + Actions.Peek().GetType().Name + "].");

                Actions.Dequeue().Exit();

                if (Actions.Count > 0) {

                    //Debug.Log("Ship [" + gameObject.name + "] doing [" + GetType().Name + "] completed action [" + Actions.Peek().GetType().Name + "].");

                    CurrentAction.Enter();
                } else {
                    //SDebug.Log("Ship [" + gameObject.name + "] doing [" + GetType().Name + "] completed ALL actions.");
                }

                stageBegan = Time.realtimeSinceStartup;
            }

            if (CurrentAction != null)
                CurrentAction.UpdateAction();
        }

    }
}