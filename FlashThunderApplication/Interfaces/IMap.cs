using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashThunderApplication
{
    interface IMap
    {
        HashSet<String> Query(String query);
    }
}
