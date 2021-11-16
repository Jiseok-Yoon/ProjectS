using ProjectS.SD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectS.DB
{
    [Serializable]
    public class BoCharacter
    {
        public SDCharacter sdCharacter;

        public BoCharacter(SDCharacter sdCharacter)
        {
            this.sdCharacter = sdCharacter;
        }


    }
}

