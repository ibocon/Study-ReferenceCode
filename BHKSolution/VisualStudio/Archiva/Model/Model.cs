using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva.Model
{
    interface Modelable
    {
        void WriteModel(MyXmlWriter xml);
    }

    enum ModelType
    {
        Wall, Floor, Fixedwindow, Flatroof, Fullstair
    }
}
