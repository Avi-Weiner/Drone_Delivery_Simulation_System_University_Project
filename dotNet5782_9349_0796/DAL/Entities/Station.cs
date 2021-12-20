using System;


namespace DO
{
    public struct Station
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        /// <summary>
        /// number of slots that are free.
        /// </summary>
        public int ChargeSlots { get; set; }
    }
}
    
