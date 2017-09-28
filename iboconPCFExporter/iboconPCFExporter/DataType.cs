using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iboconPCFExporter
{
    static public class DataType
    {
        public class Coordinate
        {
            private double X, Y, Z;

            public Coordinate(double x, double y, double z)
            {
                X = x; Y = y; Z = z;
            }

            public Coordinate(XYZ cord)
            {
                this.X = cord.X;
                this.Y = cord.Y;
                this.Z = cord.Z;
            }

            public string ToString(double unit)
            {
                return PCFWriter.FormatDecimal(X * unit) + " " + PCFWriter.FormatDecimal(Y * unit) + " " + PCFWriter.FormatDecimal(Z * unit);
            }
        }

        #region Basic Header
        public enum UNITS_BORE
        {
            INCH, MM, INCH_SIXTHEN
        }
        public enum UNITS_CO_ORDS
        {
            INCH, MM, MM_HUNDREDTHS
        }
        public enum UNITS_BOLT_DIA
        {
            INCH, MM, INCH_SIXTHEN
        }
        public enum UNITS_BOLT_LENGTH
        {
            INCH, MM, MM_HUNDREDTHS
        }
        public enum UNITS_ROTATION
        {
            NONE, DEGREES, RADIANS
        }
        public enum UNITS_STIFENESS
        {
            NONE, NM, LB_FT
        }
        public enum UNITS_WEIGHT
        {
            KGS, LBS
        }
        public enum UNITS_WEIGHT_LENGTH
        {
            NONE, INCH, METER, FEET
        }
        #endregion

        #region Pipeline Header
        public enum PIPELINE_TYPE
        {
            F1, FP, FX, FZ
        }
        public enum CONSTRUCTION_TYPE
        {
            New, Demolish, Future
        }
        public enum STRESS_CATEGORY
        {
            Yes, No, Visual, Simple, One, Two, Three
        }

        public class MNO
        {
            private int? M = null, N = null, O = null;

            public MNO(int? m, int? n, int? o)
            {
                M = m; N = n; O = o;
            }

            override public string ToString()
            {
                string ret = null;
                if (M != null)
                {
                    ret += (M + " ");
                }

                if (N != null)
                {
                    ret += (N + " ");
                }

                if (O != null)
                {
                    ret += (O + " ");
                }

                return ret.Trim();
            }
        }
        #endregion

        #region Component Attributes
        public enum KeypointType
        {
            END, BRANCH, PORT, CENTRE, CO_ORDS
        }
        public enum ConnectionType
        {
            NONE, FLANGED, PUSH_FIT, BUTT_WELD, SCREWED, PLAIN_END, SOKET_WELD
        }
        public enum GenderType
        {
            NONE, MALE, FEMALE, UNSPECIFIED
        }
        public enum TracingType
        {
            TRACING_SINGLE, TRACING_DOUBLE, TRACING_TREBLE, TRACING_QUADRUPLE, TRACING_ON, TRACING_OFF
        }
        public enum InsulationType
        {
            NONE, INSULATION_SINGLE, INSULATION_ON, INSULATION_OFF
        }

        abstract public class KeyPoint : Writable
        {
            public DataType.KeypointType Type;
            public DataType.Coordinate Coordinate;

            public KeyPoint(DataType.KeypointType type, XYZ cord)
            {
                this.Type = type;
                this.Coordinate = new Coordinate(cord);
            }

            public virtual void Write(StringBuilder writer)
            {
                string pointName = null;

                if (this.Type == KeypointType.BRANCH)
                {
                    pointName = "BRANCH" + (this as DataType.BranchPoint).Number + "-POINT";
                }
                else
                {
                    pointName = DataType.EnumString.KeypointType.GetByFirst(this.Type);
                }

                writer.Append(PCFWriter.TAB).Append(pointName + " " + this.Coordinate.ToString(PCFWriter.UnitFootToStd));

            }
        }

        public class ExternalKeyPoint : KeyPoint
        {
            public double Size;
            public DataType.ConnectionType CoonectionType = ConnectionType.NONE;
            public DataType.GenderType GenderType = GenderType.NONE;
            public DataType.InsulationType InsulationType;
            public DataType.TracingType TracingType;

            public ExternalKeyPoint(DataType.KeypointType type, XYZ cord, double size, DataType.ConnectionType endtype = ConnectionType.NONE) : base(type, cord)
            {
                this.Size = size;
                this.CoonectionType = endtype;
            }

            public override void Write(StringBuilder writer)
            {
                base.Write(writer);

                writer.Append(" ").Append(Math.Round(this.Size * 2 * PCFWriter.UnitFootToStd));

                if (this.CoonectionType != DataType.ConnectionType.NONE)
                {
                    writer.Append(" ").Append(DataType.EnumString.ConnectionType.GetByFirst(this.CoonectionType));
                }

                if (this.GenderType != DataType.GenderType.NONE)
                {
                    writer.Append(" ").Append(DataType.EnumString.GenderType.GetByFirst(this.GenderType));
                }

                if (this.InsulationType != DataType.InsulationType.NONE)
                {
                    writer.Append(" ").Append(DataType.EnumString.InsulationType.GetByFirst(this.InsulationType));
                }
            }
        }

        public class BranchPoint : ExternalKeyPoint
        {
            public int Number;

            public BranchPoint(int number, DataType.KeypointType type, XYZ cord, double size, DataType.ConnectionType endtype = ConnectionType.NONE) : base(type, cord, size, endtype)
            {
                this.Number = number;
            }
        }

        public class InternalKeyPoint : KeyPoint
        {
            public InternalKeyPoint(DataType.KeypointType type, XYZ cord) : base(type, cord)
            {

            }

            public override void Write(StringBuilder writer)
            {
                base.Write(writer);
            }
        }

        #region Allowable Attributes
        public enum CATEGORY
        {
            FABRICATION, ERECTION, OFFSHORE
        }
        public enum STATUS
        {
            STANDARD, DOTTED_DIMENSIONED, DOTTED_UNDIMENSIONED, UNDIMENSIONED
        }
        public enum TRACING
        {
            OFF, ON, SINGLE, DOUBLE, TREBLE, QUADRUPLE
        }
        public enum FLOW
        {
            One, Two, Three
        }
        public enum Material_List
        {
            Include, Exclude, Include_With_ISO
        }

        #endregion
        #endregion


        static public class EnumString
        {
            #region Basic Header
            static public BiDictionary<DataType.UNITS_BORE, string> UNITS_BORE = new BiDictionary<DataType.UNITS_BORE, string>()
            {
                {DataType.UNITS_BORE.INCH, "INCH" },
                {DataType.UNITS_BORE.MM, "MM" },
                {DataType.UNITS_BORE.INCH_SIXTHEN, "INCH-SIXTHEN" }
            };
            static public BiDictionary<DataType.UNITS_CO_ORDS, string> UNITS_CO_ORDS = new BiDictionary<DataType.UNITS_CO_ORDS, string>()
            {
                {DataType.UNITS_CO_ORDS.INCH, "INCH" },
                {DataType.UNITS_CO_ORDS.MM, "MM" },
                {DataType.UNITS_CO_ORDS.MM_HUNDREDTHS, "MM-HUNDREDTHS" }
            };
            static public BiDictionary<DataType.UNITS_BOLT_DIA, string> UNITS_BOLT_DIA = new BiDictionary<DataType.UNITS_BOLT_DIA, string>()
            {
                {DataType.UNITS_BOLT_DIA.INCH, "INCH" },
                {DataType.UNITS_BOLT_DIA.MM, "MM" },
                {DataType.UNITS_BOLT_DIA.INCH_SIXTHEN, "INCH-SIXTHEN" }
            };
            static public BiDictionary<DataType.UNITS_BOLT_LENGTH, string> UNITS_BOLT_LENGTH = new BiDictionary<DataType.UNITS_BOLT_LENGTH, string>()
            {
                {DataType.UNITS_BOLT_LENGTH.INCH, "INCH" },
                {DataType.UNITS_BOLT_LENGTH.MM, "MM" },
                {DataType.UNITS_BOLT_LENGTH.MM_HUNDREDTHS, "MM-HUNDREDTHS" }
            };
            static public BiDictionary<DataType.UNITS_ROTATION, string> UNITS_ROTATION = new BiDictionary<DataType.UNITS_ROTATION, string>()
            {
                {DataType.UNITS_ROTATION.DEGREES, "DEGREES" },
                {DataType.UNITS_ROTATION.RADIANS, "RADIANS" }
            };
            static public BiDictionary<DataType.UNITS_STIFENESS, string> UNITS_STIFENESS = new BiDictionary<DataType.UNITS_STIFENESS, string>()
            {
                {DataType.UNITS_STIFENESS.NM, "NM" },
                {DataType.UNITS_STIFENESS.LB_FT, "LB.FT" }
            };
            static public BiDictionary<DataType.UNITS_WEIGHT, string> UNITS_WEIGHT = new BiDictionary<DataType.UNITS_WEIGHT, string>()
            {
                {DataType.UNITS_WEIGHT.KGS, "KGS" },
                {DataType.UNITS_WEIGHT.LBS, "LBS" }
            };
            static public BiDictionary<DataType.UNITS_WEIGHT_LENGTH, string> UNITS_WEIGHT_LENGTH = new BiDictionary<DataType.UNITS_WEIGHT_LENGTH, string>()
            {
                {DataType.UNITS_WEIGHT_LENGTH.INCH, "INCH" },
                {DataType.UNITS_WEIGHT_LENGTH.METER, "METER" },
                {DataType.UNITS_WEIGHT_LENGTH.FEET, "FEET" }
            };
            #endregion

            #region Pipeline Header
            static public BiDictionary<DataType.PIPELINE_TYPE, string> PIPELINE_TYPE = new BiDictionary<DataType.PIPELINE_TYPE, string>()
            {
                {DataType.PIPELINE_TYPE.F1, "F1" },
                {DataType.PIPELINE_TYPE.FP, "FP" },
                {DataType.PIPELINE_TYPE.FX, "FX" },
                {DataType.PIPELINE_TYPE.FZ, "FZ" }
            };
            static public BiDictionary<DataType.CONSTRUCTION_TYPE, string> CONSTRUCTION_TYPE = new BiDictionary<DataType.CONSTRUCTION_TYPE, string>()
            {
                {DataType.CONSTRUCTION_TYPE.New, "New" },
                {DataType.CONSTRUCTION_TYPE.Demolish, "Demolish" },
                {DataType.CONSTRUCTION_TYPE.Future, "Future" }
            };
            static public BiDictionary<DataType.STRESS_CATEGORY, string> STRESS_CATEGORY = new BiDictionary<DataType.STRESS_CATEGORY, string>()
            {
                {DataType.STRESS_CATEGORY.Yes, "Yes" },
                {DataType.STRESS_CATEGORY.No, "No" },
                {DataType.STRESS_CATEGORY.Visual, "Visual" },
                {DataType.STRESS_CATEGORY.Simple, "Simple" },
                {DataType.STRESS_CATEGORY.One, "1" },
                {DataType.STRESS_CATEGORY.Two, "2" },
                {DataType.STRESS_CATEGORY.Three, "3" }
            };
            #endregion

            #region Component Attributes
            static public BiDictionary<DataType.KeypointType, string> KeypointType = new BiDictionary<DataType.KeypointType, string>()
            {
                {DataType.KeypointType.END, "END-POINT" },
                {DataType.KeypointType.BRANCH, "BRANCH-POINT" },
                {DataType.KeypointType.PORT, "PORT-POINT" },
                {DataType.KeypointType.CENTRE, "CENTRE-POINT" },
                {DataType.KeypointType.CO_ORDS, "CO-ORDS" }
            };
            static public BiDictionary<DataType.ConnectionType, string> ConnectionType = new BiDictionary<DataType.ConnectionType, string>()
            {
                { DataType.ConnectionType.FLANGED, "FL" },
                { DataType.ConnectionType.PUSH_FIT, "PF" },
                { DataType.ConnectionType.BUTT_WELD, "BW" },
                { DataType.ConnectionType.SCREWED, "SC" },
                { DataType.ConnectionType.PLAIN_END, "PL" },
                { DataType.ConnectionType.SOKET_WELD, "SW" }
            };
            static public BiDictionary<DataType.GenderType, string> GenderType = new BiDictionary<DataType.GenderType, string>()
            {
                {DataType.GenderType.MALE, "MALE" },
                {DataType.GenderType.FEMALE, "FEMALE" },
                {DataType.GenderType.UNSPECIFIED, "UNSPECIFIED" }
            };
            static public BiDictionary<DataType.TracingType, string> TracingType = new BiDictionary<DataType.TracingType, string>()
            {
                {DataType.TracingType.TRACING_SINGLE, "TRACING-SINGLE" },
                {DataType.TracingType.TRACING_DOUBLE, "TRACING-DOUBLE" },
                {DataType.TracingType.TRACING_TREBLE, "TRACING-TREBLE" },
                {DataType.TracingType.TRACING_QUADRUPLE, "TRACING-QUADRUPLE" },
                {DataType.TracingType.TRACING_ON, "TRACING-ON" },
                {DataType.TracingType.TRACING_OFF, "TRACING-OFF" }
            };
            static public BiDictionary<DataType.InsulationType, string> InsulationType = new BiDictionary<DataType.InsulationType, string>()
            {
                {DataType.InsulationType.INSULATION_SINGLE, "INSULATION-SINGLE" },
                {DataType.InsulationType.INSULATION_ON, "INSULATION-ON" },
                {DataType.InsulationType.INSULATION_OFF, "INSULATION-OFF" }
            };
            #region Allowable Attributes
            static public BiDictionary<DataType.CATEGORY, string> CATEGORY = new BiDictionary<DataType.CATEGORY, string>()
            {
                {DataType.CATEGORY.FABRICATION, "FABRICATION" },
                {DataType.CATEGORY.ERECTION, "ERECTION" },
                {DataType.CATEGORY.OFFSHORE, "OFFSHORE" }
            };
            #endregion
            #endregion
        }
    }
    
}
