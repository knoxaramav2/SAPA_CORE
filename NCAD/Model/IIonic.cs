using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAD.Model
{
    public interface IIonic
    {
        public float INaC { get; set; } //mmol/L
        public float IKC { get; set; } //mmol/L
    }
}
