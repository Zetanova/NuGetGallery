﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuGetGallery
{
    public interface IPackageOwnershipManagementService
    {
        /// <summary>
        /// Add the user as an owner to the package. Also add the package registration
        /// to the reserved namespaces owned by this user if the Id matches any of the 
        /// reserved prefixes. Also mark the package registration as verified if it matches any
        /// of the user owned reserved namespaces.
        /// </summary>
        /// <param name="packageRegistration">The package registration that is intended to get ownership.</param>
        /// <param name="user">The user to add as an owner to the package.</param>
        Task AddPackageOwnerAsync(PackageRegistration packageRegistration, User user);

        /// <summary>
        /// Add the pending ownership request.
        /// </summary>
        /// <param name="packageRegistration">The package registration that has pending ownership request.</param>
        /// <param name="requestingOwner">The user to requesting to add the pending owner.</param>
        /// <param name="newOwner">The user to be added for from pending ownership.</param>
        Task<PackageOwnerRequest> AddPackageOwnershipRequestAsync(PackageRegistration packageRegistration, User requestingOwner, User newOwner);

        /// <summary>
        /// Remove the user as from the list of owners of the package. Also remove the package registration
        /// from the reserved namespaces owned by this user if the Id matches any of the reserved prefixes
        /// and the user is the only package owner that owns the namespace that matches the package registration.
        /// </summary>
        /// <param name="packageRegistration">The package registration that is intended to get ownership.</param>
        /// <param name="requestingUser">The user requesting to remove an owner from the package.</param>
        /// <param name="userToBeRemoved">The user to remove as an owner from the package.</param>
        Task RemovePackageOwnerAsync(PackageRegistration packageRegistration, User requestingUser, User userToBeRemoved);

        /// <summary>
        /// Remove the pending ownership request.
        /// </summary>
        /// <param name="packageRegistration">The package registration that has pending ownership request.</param>
        /// <param name="newOwner">The user to be removed from pending ownership.</param>
        Task DeletePackageOwnershipRequestAsync(PackageRegistration packageRegistration, User newOwner);

        /// <summary>
        /// Gets <see cref="PackageOwnerRequest"/>s that match the provided conditions.
        /// </summary>
        /// <param name="package">If nonnull, only returns <see cref="PackageOwnerRequest"/>s that are for this package.</param>
        /// <param name="requestingOwner">If nonnull, only returns <see cref="PackageOwnerRequest"/>s that were requested by this owner.</param>
        /// <param name="newOwner">If nonnull, only returns <see cref="PackageOwnerRequest"/>s that are for this user to become an owner.</param>
        /// <returns>An <see cref="IEnumerable{PackageOwnerRequest}"/> containing all objects that matched the conditions.</returns>
        IEnumerable<PackageOwnerRequest> GetPackageOwnershipRequests(PackageRegistration package = null, User requestingOwner = null, User newOwner = null);

        /// <summary>
        /// Checks if the pending owner has a request for this package which matches the specified token.
        /// </summary>
        /// <param name="package">Package associated with the request.</param>
        /// <param name="pendingOwner">Pending owner for the request.</param>
        /// <param name="token">Token generated for the owner request.</param>
        /// <returns>The <see cref="PackageOwnerRequest"/> if one exists or null otherwise.</returns>
        PackageOwnerRequest GetPackageOwnershipRequest(PackageRegistration package, User pendingOwner, string token);

    }
}