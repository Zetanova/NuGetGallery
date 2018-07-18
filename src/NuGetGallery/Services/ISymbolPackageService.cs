﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGet.Packaging;
using NuGetGallery.Packaging;

namespace NuGetGallery
{
    /// <summary>
    /// Business logic related to <see cref="SymbolPackage"/>.
    /// </summary>
    public interface ISymbolPackageService
    {
        /// <summary>
        /// Gets all the symbol packages associated with the Package ID and version
        /// </summary>
        /// <param name="id">The package ID.</param>
        /// <param name="version">The package version.</param>
        /// <returns></returns>
        IEnumerable<SymbolPackage> FindSymbolPackageByIdAndVersion(string id, string version);

        /// <summary>
        /// Populate the related database tables to create the specified symbol package.
        /// </summary>
        /// <remarks>
        /// This method doesn't upload the package binary to the blob storage. The caller must do it after this call.
        /// </remarks>
        /// <param name="symbolPackage">The package to be created.</param>
        /// <param name="packageStreamMetadata">The package stream's metadata.</param>
        /// <param name="owner">The owner of the package</param>
        /// <param name="currentUser">The user that pushed the package on behalf of <paramref name="owner"/></param>
        /// <returns>The created symbol package entity.</returns>
        Task<SymbolPackage> CreateSymbolPackageAsync(PackageArchiveReader symbolPackage, PackageStreamMetadata packageStreamMetadata, User owner, User currentUser);

        /// <summary>
        /// Update the status of the symbol package.
        /// </summary>
        /// <param name="status">Enum value for <see cref="PackageStatus"/></param>
        /// <returns>Awaitable task</returns>
        Task SetSymbolPacakgeStatus(PackageStatus status);
    }
}