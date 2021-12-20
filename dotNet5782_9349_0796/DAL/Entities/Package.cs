using System;


namespace DO
{
    public struct Package
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priority { get; set; }
        /// <summary>
        /// Same as creation time
        /// </summary>
        public DateTime Requested { get; set; }
        /// <summary>
        /// 0 if not allocated
        /// </summary>
        public int DroneId { get; set; }
        public Nullable<DateTime> Scheduled { get; set; }
        public Nullable<DateTime> PickedUp { get; set; }
        public Nullable<DateTime> Delivered { get; set; }
    }
}

