﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lerisa
{
    public class DataController : MonoBehaviour
    {

        public RoundData[] allRoundData;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public RoundData GetCurrentRoundData()
        {
            return allRoundData[0];
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}