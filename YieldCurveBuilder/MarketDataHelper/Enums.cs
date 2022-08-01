using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public enum EIndexKind
    { 
        Libor,
        Tonar,
    }

    public enum EnumDcc
    { 
        Act360,
        Act365,
    }

    public enum EnumBdc
    { 
        Following,
        ModFollowing,
        Preceding,
    }

    public enum EProduct
    { 
        Depo,
        Fra,
        Swap,
        Ois,
    }
}
