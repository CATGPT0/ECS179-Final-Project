using System;
using System.Collections.Generic;

namespace Plugins.KennethDevelops.Events {
    public static class EventUtilities {
        
        public static List<Type> GetDerivedFromBaseEvent() {
            List<Type> derivedTypes = new List<Type>();

            // Get all assemblies in the current application domain
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies) {
                // Get all types from the assembly
                var types = assembly.GetTypes();

                foreach (var type in types) {
                    // Check if the type is a subclass of any generic BaseEvent<>
                    if (type.BaseType                            != null && type.BaseType.IsGenericType &&
                        type.BaseType.GetGenericTypeDefinition() == typeof(BaseEvent<>)) {
                        derivedTypes.Add(type);
                    }
                }
            }

            return derivedTypes;
        }
        
    }
}