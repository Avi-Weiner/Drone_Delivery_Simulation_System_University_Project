using System;

namespace IDAL
{
    namespace DO
    {
        #region Parcel Struct
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            //WeightCategories Weight; WeightCategories needs to be defined
            //Priorities Priority; Priority needs to be defined
            public DateTime dateTime { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            //constructor for Parcel here
        }
        #endregion
    }
}
