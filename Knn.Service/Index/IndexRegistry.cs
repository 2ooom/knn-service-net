using HNSW.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knn.Service.Index
{
    public class IndexRegistry
    {
        private Dictionary<int, SmallWorld<float[], float>> _indices;
        public IndexRegistry()
        {
            
        }
    }
}
