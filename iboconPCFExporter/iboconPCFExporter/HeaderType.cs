using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iboconPCFExporter
{
    public class BasicHeader : Writable
    {
        //필수항목 - 국제표준측정법을 디폴트 설정으로 한다.
        public DataType.UNITS_BORE Units_Bore = DataType.UNITS_BORE.MM;
        public DataType.UNITS_CO_ORDS Units_Co_Ords = DataType.UNITS_CO_ORDS.MM;
        public DataType.UNITS_BOLT_DIA Units_Bolt_Dia = DataType.UNITS_BOLT_DIA.MM;
        public DataType.UNITS_BOLT_LENGTH Units_Bolt_Length = DataType.UNITS_BOLT_LENGTH.MM;
        public DataType.UNITS_WEIGHT Units_Weight = DataType.UNITS_WEIGHT.KGS;
        //부가항목
        public DataType.UNITS_ROTATION Units_Rotation = DataType.UNITS_ROTATION.NONE;
        public DataType.UNITS_STIFENESS Units_Stifeness = DataType.UNITS_STIFENESS.NONE;
        public DataType.UNITS_WEIGHT_LENGTH Units_Weight_Length = DataType.UNITS_WEIGHT_LENGTH.NONE;

        public void Write(StringBuilder writer)
        {
            writer.Append("ISOGEN-FILES ISOGEN.FLS").AppendLine();

            writer.Append("UNITS-BORE " + DataType.EnumString.UNITS_BORE.GetByFirst(this.Units_Bore)).AppendLine();
            writer.Append("UNITS-CO-ORDS " + DataType.EnumString.UNITS_CO_ORDS.GetByFirst(this.Units_Co_Ords)).AppendLine();
            writer.Append("UNITS-BOLT-DIA " + DataType.EnumString.UNITS_BOLT_DIA.GetByFirst(this.Units_Bolt_Dia)).AppendLine();
            writer.Append("UNITS-BOLT-LENGTH " + DataType.EnumString.UNITS_BOLT_LENGTH.GetByFirst(this.Units_Bolt_Length)).AppendLine();

            if (this.Units_Rotation != DataType.UNITS_ROTATION.NONE)
            {
                writer.Append("UNITS-ROTATION " + DataType.EnumString.UNITS_ROTATION.GetByFirst(this.Units_Rotation)).AppendLine();
            }
            if (this.Units_Stifeness != DataType.UNITS_STIFENESS.NONE)
            {
                writer.Append("UNITS-STIFENESS " + DataType.EnumString.UNITS_STIFENESS.GetByFirst(this.Units_Stifeness)).AppendLine();
            }

            writer.Append("UNITS-WEIGHT " + DataType.EnumString.UNITS_WEIGHT.GetByFirst(this.Units_Weight)).AppendLine();

            if (this.Units_Weight_Length != DataType.UNITS_WEIGHT_LENGTH.NONE)
            {
                writer.Append("UNITS-WEIGHT-LENGTH " + DataType.EnumString.UNITS_WEIGHT_LENGTH.GetByFirst(this.Units_Weight_Length)).AppendLine();
            }
        }
    }

    public class PipelineHeader : Writable
    {
        //필수항목 - 라인 참조나 ID
        public string Pipeline_Reference = "DEFUALT";
        //부가항목
        #region Specification Attributes
        public string Piping_Spec = "CS150";
        public string TRACING_SPEC;
        public string INSULATION_SPEC;
        public string PAINTING_SPEC;
        public List<string> MISC_SPEC = new List<string>(5);
        public string JACKET_SPEC;
        #endregion
        #region Miscellaneous Attributes
        public string REVISION;
        public string PROJECT_IDENTIFIER;
        public string AREA;
        public string DATE_DMY;
        public string NOMINAL_RATING;
        public string BEND_RADIUS;
        public List<string> ITEM_ATTRUBITE = new List<string>(10);
        public string PIPELINE_TEMP;
        public DataType.PIPELINE_TYPE PIPELINE_TYPE;
        public string SPECIFIC_GRAVITY;
        public string SPOOL_PREFIX;
        public List<string> ATTRIBUTE = new List<string>(200);
        public string CLEANING_REQUIREMENT;
        public string PAINT_COLOUR;
        public DataType.CONSTRUCTION_TYPE CONSTRUCTION_TYPE;
        public string DESIGN_PRESSURE;
        public string DEGISN_TEMPERATURE;
        public string ENGINEERING_WORK_PACKAGE;
        public string INSTALLATION_WORK_PACKAGE;
        public string FLUID_CODE;
        public string HANDOVER_SYSTEM_ID;
        public bool TRACING_REQUIREMENT;
        public string TEST_PRESSURE;
        public double INSULATION_THICKNESS;
        public double MAIN_NS;
        public string OPERATING_PRESSURE;
        public string PID_DRAWING_NUMBER;
        public string LINE_ID;
        public string ALT_DESIGN_PRESSURE;
        public string ALT_DESIGN_TEMPERATURE;
        public string DESIGN_CODE;
        public string FLUID_PHASE;
        public string FROM;
        public string HANDOVER_SUBSYSTEM_ID;
        public string INSULATION_SPEC_NUMBER;
        public string NDT_REQUIREMENT;
        public int PID_REVISION;
        public string PROCUREMENT_WORK_PACKAGE;
        public string STRESS_CATEGORY;
        public string STRESS_PACKAGE;
        public string TEST_MEDIUM;
        public string TO;
        public bool PWHT_REQUIREMENT;
        public int SEQUENCE_NUMBER;
        public int CONSTRUCTION_WORK_PACKAGE;
        public string MATERIAL_OF_CONSTRUCTION;
        #endregion
        #region Weld Prefixes
        public string WELD_PREFIX_GENERAL;
        public string WELD_PREFIX_FABRICATION;
        public string WELD_PREFIX_ERECTION;
        public string WELD_PREFIX_OFFSHORE;
        public string SUPPORT_WELD_PREFIX_FABRICATION;
        public string SUPPORT_WELD_PREFIX_ERECTION;
        public string sUPPORT_WELD_PREFIX_OFFSHORE;
        #endregion
        #region Drawing Control Parameters
        public DataType.Coordinate Start_Co_Ords = new DataType.Coordinate(0, 0, 0);
        public DataType.MNO HIGHEST_PART_NUMBER;
        public DataType.MNO HIGHEST_UNIQUE_IDENTIFIER;
        public DataType.MNO HIGHEST_SPOOL_NUMBER;
        public DataType.MNO HIGHEST_WELD_NUMBER;
        public DataType.MNO HIGHEST_WELD_SUPPORT_NUMBER;
        public DataType.MNO HIGHEST_ASSEMBLY_NUMBER;
        #endregion
        #region Coordinate Offset
        public DataType.Coordinate OFFSET_IMPERIAL;
        public DataType.Coordinate OFFSET_METRIC;

        //TODO: (하) 현재 필수적으로 필요한 부분만 쓰도록 작성되어 있다. 필요에 따라, 더 많은 옵션을 쓰도록 확장해야 한다.
        public void Write(StringBuilder writer)
        {
            writer.Append("PIPELINE-REFERENCE " + this.Pipeline_Reference).AppendLine();
            writer.Append(PCFWriter.TAB).Append("PIPING-SPEC " + this.Piping_Spec).AppendLine();
            writer.Append(PCFWriter.TAB).Append("START-CO-ORDS " + this.Start_Co_Ords.ToString(PCFWriter.UnitFootToStd)).AppendLine();
        }
        #endregion
    }
}
