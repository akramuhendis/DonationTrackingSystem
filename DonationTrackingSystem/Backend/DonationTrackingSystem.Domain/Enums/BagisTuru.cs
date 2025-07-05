using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Domain.Enums
{
    /// <summary>
    /// Bağışın türünü belirten enum
    /// </summary>
    public enum BagisTuru
    {
        /// <summary>
        /// Nakit olarak yapılan bağış
        /// </summary>
        Nakit = 1,
        /// <summary>
        /// Eşya olarak yapılan bağış
        /// </summary>
        Esya = 2,
        /// <summary>
        /// Hizmet olarak yapılan bağış
        /// </summary>
        Hizmet = 3
    }
}
