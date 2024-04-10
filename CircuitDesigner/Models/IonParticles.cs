using System.ComponentModel;
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
    

    public class Ion
    {
        //Sodium
        public const float DEF_MMol_INT_NA = 142f;
        public const float DEF_MMol_EXT_NA = 10f;
        public const int DEF_PARTS_NA = 1;
        //Potassium
        public const float DEF_MMol_INT_K = 5f;
        public const float DEF_MMol_EXT_K = 150f;
        public const int DEF_PARTS_K =90;
        //Calcium
        public const float DEF_MMol_INT_CA = 5f;
        public const float DEF_MMol_EXT_CA = .001f;
        public const int DEF_PARTS_CA = 1;
        //Chloride
        public const float DEF_MMol_INT_CL = 103f;
        public const float DEF_MMol_EXT_CL = 4f;
        public const int DEF_PARTS_CL = 8;

        public enum IonType
        {
            SODIUM,
            POTASSIUM,
            CHLORIDE,
            CALCIUM
        }

        public IonType Type { get; protected set; }
        public int Parts { get; set; }
        public float Concentration { get; set; }

        [JsonConstructor]
        public Ion() { }

        public Ion(IonType type, int parts, float concentraion) {
            Type = type;
            Parts = parts;
            Concentration = concentraion;
        }

        //Nernst equation
        public static float ConcentrationCharge(IonType type, float inConcentration,  float outConcentration) {

            float ionCharge = type == IonType.CALCIUM ? 30.75f : 61.5f;
            var charge = (float)(ionCharge * Math.Log10(inConcentration/outConcentration));
            var ret = type == IonType.CHLORIDE ? -charge : charge;
            return ret;
        }
    }

    public class Sodium: Ion
    {
        [JsonConstructor]
        public Sodium() { }

        public Sodium(int parts, float concentration)
        {
            Type = IonType.SODIUM;
            Parts = parts;
            Concentration = concentration;
        }
    
        public void SetParts(int value)
        {
            Parts = value;
        }

        public void SetConcentration(float value)
        {
            Concentration = value;
        }
    }

    public class Potassium : Ion
    {
        [JsonConstructor]
        public Potassium() { }

        public Potassium(int parts, float concentration)
        {
            Type = IonType.POTASSIUM;
            Parts = parts;
            Concentration = concentration;
        }
    }

    public class Chloride : Ion
    {
        [JsonConstructor]
        public Chloride() { }

        public Chloride(int parts, float concentration)
        {
            Type = IonType.CHLORIDE;
            Parts = parts;
            Concentration = concentration;
        }
    }

    public class Calcium : Ion
    {
        [JsonConstructor]
        public Calcium() { }

        public Calcium(int parts, float concentration)
        {
            Type = IonType.SODIUM;
            Parts = parts;
            Concentration = concentration;
        }
    }

    public class IonState
    {
        public Sodium Na { get; set; }
        public Potassium K { get; set; }
        public Calcium Ca { get; set; }
        public Chloride Cl { get; set; }

        [JsonConstructor]
        public IonState() { }

        public IonState(
            float naC, int naParts,
            float kC, int kParts,
            float caC, int caParts,
            float clC, int clParts
            )
        {
            Na = new(naParts, naC);
            K = new(kParts, kC);
            Ca = new(caParts, caC);
            Cl = new(clParts, clC);
        }

        public static IonState DefaultExternalState()
        {
            return new IonState(
                Ion.DEF_MMol_EXT_NA, Ion.DEF_PARTS_NA,
                Ion.DEF_MMol_EXT_K, Ion.DEF_PARTS_K,
                Ion.DEF_MMol_EXT_CA, Ion.DEF_PARTS_CA,
                Ion.DEF_MMol_EXT_CL, Ion.DEF_PARTS_CL
                );
        }

        public static IonState DefaultInternalState()
        {
            return new IonState(
                Ion.DEF_MMol_INT_NA, Ion.DEF_PARTS_NA,
                Ion.DEF_MMol_INT_K, Ion.DEF_PARTS_K,
                Ion.DEF_MMol_INT_CA, Ion.DEF_PARTS_CA,
                Ion.DEF_MMol_INT_CL, Ion.DEF_PARTS_CL
                );
        }
    }
}
