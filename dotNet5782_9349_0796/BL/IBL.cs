using System;

namespace BL
{
    public interface IBL
    {
        void AddBaseStation(int Id, string name, double location, int slots);
        void AddDrone(int manufactureId, string model, /*enum wieght*/ int chargingStation);
         void AddCustomer();
        void AddPackage();
        void updateDrone(int Id, string Model);
    }
}
