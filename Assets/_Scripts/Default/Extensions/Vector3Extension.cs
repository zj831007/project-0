using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project0
{
    public static class Vector3Extension
    {
        public static float AngleFromXZ(this Vector3 vector)
        {
            return Vector3.Angle(vector, new Vector3(vector.x, 0f, vector.z));
        }
    }
}
