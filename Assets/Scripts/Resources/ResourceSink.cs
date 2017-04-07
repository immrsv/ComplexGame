﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources {
    [DisallowMultipleComponent]
    public class ResourceSink : MonoBehaviour { 

        public float seedDelay = 10;
        public List<ResourceType> Seeds;

        private float nextSeed;
        private ResourceContainer container;

        // Use this for initialization
        void Start() {
            container = GetComponent<ResourceContainer>();
        }

        // Update is called once per frame
        void Update() {
            if (Time.realtimeSinceStartup >= nextSeed) {
                nextSeed = Time.realtimeSinceStartup + seedDelay;
                foreach (var resource in Seeds) {
                    container.Items[resource] = 0;
                }
            }
        }
    }
}