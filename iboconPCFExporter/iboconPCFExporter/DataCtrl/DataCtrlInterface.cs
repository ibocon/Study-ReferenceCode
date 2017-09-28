using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iboconPCFExporter.DataCtrl
{
    public interface DataCtrlInterface
    {
        //기본적으로 데이터를 상호작용하는 함수
        void Import(PCFData data);
        void Export(PCFData data);
        //Material 정보를 검색해 넘겨주는 함수
        IDictionary<int, MaterialData> GetMaterials();
    }
}
