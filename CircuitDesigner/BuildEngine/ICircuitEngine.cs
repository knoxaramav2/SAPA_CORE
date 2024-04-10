using CircuitDesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitDesigner.BuildEngine
{
    internal interface ICircuitEngine
    {
        public CircuitError[] Verify();
        public string? Build();
    }

    enum CircuitErrorCode
    {
        Misc,
        NullConnector,
        NullNode,
        OutOfSpec,
    }

    struct CircuitError
    {
        public readonly CircuitErrorCode ErrorCode;
        public readonly string Msg;

        public CircuitError(string msg, CircuitErrorCode code)
        {
            Msg = msg;
            ErrorCode = code;
        }
    }
}
