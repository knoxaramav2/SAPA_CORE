using System.Text.Json.Serialization;

namespace CircuitDesigner.Models
{
    public enum TransmitterFX
    {
        EXCITE,
        INHIBIT
    }

    public class Transmitter
    {
        public string Name { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();

        public float ChargeMultipler { get; set; }
        public TransmitterFX Effect;

        [JsonConstructor]
        public Transmitter() { }

        public Transmitter(string name, TransmitterFX effect, float chargeCoef = 0.0f)
        {
            Name = name;
            Effect = effect;
            ChargeMultipler = chargeCoef;
        }
    }
}
