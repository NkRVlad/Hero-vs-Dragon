using BusinessAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace BusinessAccessLayer.HitService
{
    public interface IHitService
    {
        string HitDragon(HitDTO hitDragon);
        int ResultImpactForces(int gun);
    }
}
