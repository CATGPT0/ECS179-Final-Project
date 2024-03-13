using UnityEngine;

namespace Plugins.KennethDevelops.Extensions {
    public static class LayerExtensions {
        
        /// <summary>
        /// Returns if the LayerMask includes the @layer value.
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool IncludesLayer(this LayerMask layerMask, int layer) {
            return ((1 << layer) & layerMask.value) != 0;
        }
        
    }
}