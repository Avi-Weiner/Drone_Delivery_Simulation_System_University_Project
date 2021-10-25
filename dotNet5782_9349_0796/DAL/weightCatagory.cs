using System;

namespace IDAL
{
    namespace DO
    {
        public struct WeightCategory
        {
            public enum Weight { light, medium, heavy }
            public Weight Myweight { get; set; }
            
        }
    }
}