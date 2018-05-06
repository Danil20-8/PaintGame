using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialInput
{
    public interface ISpecialInput
    {
        void AddProfile(InputProfile profile, int device = 0);
        void RemoveProfile(InputProfile profile, int device = 0);
    }
}
