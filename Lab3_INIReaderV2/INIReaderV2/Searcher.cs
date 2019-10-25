using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReaderV2
{
    class Searcher <T>
    {
        List<Section> sections;

        public Searcher(List<Section> InputedSections) 
        {
            this.sections = InputedSections;
        }
    }
}
