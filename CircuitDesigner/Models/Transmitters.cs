﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.Models
{
    internal enum TransmitterFX
    {
        EXCITE,
        INHIBIT
    }

    internal class Transmitter(string name, TransmitterFX effect,
        float? chargeCoef=null)
    {
        public string Name { get; set; } = name;
        public Guid ID { get; set; } = Guid.NewGuid();

        public float ChargeMultipler { get; set; } = chargeCoef ?? 0.0f;
        public TransmitterFX Effect = effect;
    }
}