using System.Collections.Generic;
using System.Linq;

namespace Vantage.Identity.API.Services
{
    #region Interfaces

    public interface IClaimFlatteningEngine
    {
        IEnumerable<string> FlattenPermissions(IEnumerable<string> assignedRoles, IEnumerable<string> directUserPermissions);
    }

    #endregion

    #region Implementations

    public class ClaimFlatteningEngine(IAccessManagementStore store) : IClaimFlatteningEngine
    {
        public IEnumerable<string> FlattenPermissions(IEnumerable<string> assignedRoles, IEnumerable<string> directUserPermissions)
        {
            var hierarchyMap = store.GetPermissionHierarchy();
            var rolePermissions = store.GetPermissionsForRoles(assignedRoles);

            var initialPermissions = rolePermissions.Concat(directUserPermissions ?? Enumerable.Empty<string>());

            var flattenedSet = new HashSet<string>();
            var processingQueue = new Queue<string>(initialPermissions);
            var processedParents = new HashSet<string>();

            while (processingQueue.Count > 0)
            {
                var currentPermission = processingQueue.Dequeue();

                // Check if this permission is a Parent/Macro
                if (hierarchyMap.TryGetValue(currentPermission, out var children))
                {
                    // It is a parent. Track it to prevent infinite recursive loops, 
                    // but DO NOT add it to the final flattened set.
                    if (processedParents.Add(currentPermission))
                    {
                        foreach (var child in children)
                        {
                            processingQueue.Enqueue(child);
                        }
                    }
                }
                else
                {
                    // It is an atomic leaf permission. Add it to the final JWT payload.
                    flattenedSet.Add(currentPermission);
                }
            }

            return flattenedSet;
        }
    }

    #endregion
}