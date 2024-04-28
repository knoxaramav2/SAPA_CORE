using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAD.Model
{
    public interface ICopyable<T>
    {
        T Copy();
    }
}
