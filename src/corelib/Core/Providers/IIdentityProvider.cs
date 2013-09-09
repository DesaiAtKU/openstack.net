﻿using System;
using System.Collections.Generic;
using net.openstack.Core.Domain;
using net.openstack.Core.Exceptions.Response;

namespace net.openstack.Core.Providers
{
    /// <summary>
    /// Represents a provider for the OpenStack Identity Service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/">OpenStack Identity Service API v2.0 Reference</seealso>
    public interface IIdentityProvider
    {
        /// <summary>
        /// Authenticates the user for the specified identity.
        /// </summary>
        /// <remarks>
        /// This method always authenticates to the server, even if an unexpired, cached <see cref="UserAccess"/>
        /// is available for the specified identity.
        /// </remarks>
        /// <param name="identity">The identity of the user to authenticate. If this value is <c>null</c>, the authentication is performed with the <see cref="DefaultIdentity"/>.</param>
        /// <returns>A <see cref="UserAccess"/> object containing the authentication token, service catalog and user data.</returns>
        /// <exception cref="NotSupportedException">If the provider does not support the given <paramref name="identity"/> type.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the authentication request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        UserAccess Authenticate(CloudIdentity identity = null);

        /// <summary>
        /// Gets the authentication token for the specified identity. If necessary, the identity is authenticated
        /// on the server to obtain a token.
        /// </summary>
        /// <remarks>
        /// If <paramref name="forceCacheRefresh"/> is <c>false</c> and a cached <see cref="IdentityToken"/>
        /// is available for the specified <paramref name="identity"/>, this method may return the cached
        /// value without performing an authentication against the server. If <paramref name="forceCacheRefresh"/>
        /// is <c>true</c>, this method returns the equivalent of the following statement.
        ///
        /// <para><c>provider.<see cref="Authenticate">Authenticate</see>(<paramref name="identity"/>).<see cref="UserAccess.Token"/></c></para>
        /// </remarks>
        /// <param name="identity">The identity of the user to authenticate. If this value is <c>null</c>, the authentication is performed with the <see cref="DefaultIdentity"/>.</param>
        /// <param name="forceCacheRefresh">If <c>true</c>, the user is always authenticated against the server; otherwise a cached <see cref="IdentityToken"/> may be returned.</param>
        /// <returns>The user's authentication token.</returns>
        /// <exception cref="NotSupportedException">If the provider does not support the given <paramref name="identity"/> type.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the authentication request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        IdentityToken GetToken(CloudIdentity identity = null, bool forceCacheRefresh = false);

        /// <summary>
        /// Lists global roles for a specified user. Excludes tenant roles.
        /// </summary>
        /// <param name="userId">The user's ID. The behavior is unspecified if this value is not obtained from <see cref="User.Id"/>.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>A collection of <see cref="Role"/> objects describing the roles for the specified user.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="userId"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="userId"/> is empty.</exception>
        /// <exception cref="NotSupportedException">If the provider does not support the given <paramref name="identity"/> type.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/GET_listUserGlobalRoles_v2.0_users__user_id__roles_User_Operations.html">List User Global Roles (OpenStack Identity Service API v2.0 Reference)</seealso>
        IEnumerable<Role> GetRolesByUser(string userId, CloudIdentity identity = null);

        /// <summary>
        /// Lists all users for the account.
        /// </summary>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>A collection of <see cref="User"/> objects describing the users for the account.</returns>
        /// <exception cref="NotSupportedException">
        /// If the provider does not support the <see href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/Admin_API_Service_Developer_Operations-d1e1357.html">OS-KSADM Admin Extension</see>.
        /// <para>-or-</para>
        /// <para>If the provider does not support the given <paramref name="identity"/> type.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/GET_listUsers_v2.0_users_.html">List Users (OpenStack Identity Service API v2.0 Reference)</seealso>
        IEnumerable<User> ListUsers(CloudIdentity identity = null);

        /// <summary>
        /// Gets the details for the user with the specified username.
        /// </summary>
        /// <param name="name">The username.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>The <see cref="User"/> details.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="name"/> is empty.</exception>
        /// <exception cref="NotSupportedException">If the provider does not support the given <paramref name="identity"/> type.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/GET_getUserByName_v2.0_users__User_Operations.html">Get User Information by Name (OpenStack Identity Service API v2.0 Reference)</seealso>
        User GetUserByName(string name, CloudIdentity identity = null);

        /// <summary>
        /// Gets the details for the user with the specified ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>The <see cref="User"/> details.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="id"/> is empty.</exception>
        /// <exception cref="NotSupportedException">If the provider does not support the given <paramref name="identity"/> type.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/GET_getUserById_v2.0_users__user_id__User_Operations.html">Get User Information by ID (OpenStack Identity Service API v2.0 Reference)</seealso>
        User GetUser(string id, CloudIdentity identity = null);

        /// <summary>
        /// Adds a user to the account.
        /// </summary>
        /// <remarks>
        /// The returned <see cref="NewUser"/> object will contain the password of the created user.
        /// If no <see cref="NewUser.Password"/> is specified in <paramref name="user"/>, the returned
        /// password will be an automatically generated password from the server.
        /// <note type="warning">
        /// After this call, there is no way to retrieve the password for a user. If the password was
        /// auto-generated by the server, make sure to either store the returned value or provide the
        /// information to the user for later use.
        /// </note>
        /// </remarks>
        /// <param name="user">A <see cref="NewUser"/> object containing the details of the user to create.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>A <see cref="NewUser"/> object containing the details of the created user.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="user"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="user"/>.<see cref="NewUser.Username"/> is <c>null</c> or empty.</exception>
        /// <exception cref="NotSupportedException">
        /// If the provider does not support the <see href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/Admin_API_Service_Developer_Operations-d1e1357.html">OS-KSADM Admin Extension</see>.
        /// <para>-or-</para>
        /// <para>If the provider does not support the given <paramref name="identity"/> type.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If <paramref name="user"/>.<see cref="NewUser.Id"/> is not <c>null</c> (i.e. <paramref name="user"/> represents a user that already exists).
        /// <para>-or-</para>
        /// <para>If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</para>
        /// </exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_addUser_v2.0_users_.html">Add User (OpenStack Identity Service API v2.0 Reference)</seealso>
        NewUser AddUser(NewUser user, CloudIdentity identity = null);

        /// <summary>
        /// Updates the details for the specified user.
        /// </summary>
        /// <remarks>
        /// The ID of the user to update is specified in <paramref name="user"/>.<see cref="User.Id"/>.
        /// The other fields of <paramref name="user"/> are either <c>null</c> to keep the existing values
        /// or non-null to specify an updated value. The returned <see cref="User"/> instance contains
        /// the complete details of the updated user.
        /// </remarks>
        /// <param name="user">The <see cref="User"/> details to update.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>A <see cref="User"/> object containing the details of the updated user.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="user"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="user"/>.<see cref="User.Id"/> is <c>null</c> or empty.</exception>
        /// <exception cref="NotSupportedException">
        /// If the provider does not support the <see href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/Admin_API_Service_Developer_Operations-d1e1357.html">OS-KSADM Admin Extension</see>.
        /// <para>-or-</para>
        /// <para>If the provider does not support the given <paramref name="identity"/> type.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_updateUser_v2.0_users__userId__.html">Update User (OpenStack Identity Service API v2.0 Reference)</seealso>
        User UpdateUser(User user, CloudIdentity identity = null);

        /// <summary>
        /// Deletes the specified user from the account.
        /// </summary>
        /// <param name="userId">The user ID. The behavior is unspecified if this is not obtained from <see cref="User.Id"/> or <see cref="NewUser.Id"/>.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns><c>true</c> if the user was successfully deleted; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="userId"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="userId"/> is empty.</exception>
        /// <exception cref="NotSupportedException">
        /// If the provider does not support the <see href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/Admin_API_Service_Developer_Operations-d1e1357.html">OS-KSADM Admin Extension</see>.
        /// <para>-or-</para>
        /// <para>If the provider does not support the given <paramref name="identity"/> type.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/DELETE_deleteUser_v2.0_users__userId__.html">Delete User (OpenStack Identity Service API v2.0 Reference)</seealso>
        bool DeleteUser(string userId, CloudIdentity identity = null);

        /// <summary>
        /// Lists the credentials for the specified user.
        /// </summary>
        /// <param name="userId">The user ID. The behavior is unspecified if this is not obtained from <see cref="User.Id"/> or <see cref="NewUser.Id"/>.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>List of <see cref="UserCredential"/> objects describing the credentials of the specified user.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="userId"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="userId"/> is empty.</exception>
        /// <exception cref="NotSupportedException">
        /// If the provider does not support the <see href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/Admin_API_Service_Developer_Operations-d1e1357.html">OS-KSADM Admin Extension</see>.
        /// <para>-or-</para>
        /// <para>If the provider does not support the given <paramref name="identity"/> type.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/GET_listCredentials_v2.0_users__userId__OS-KSADM_credentials_.html">List Credentials (OpenStack Identity Service API v2.0 Reference)</seealso>
        IEnumerable<UserCredential> ListUserCredentials(string userId, CloudIdentity identity = null);

        /// <summary>
        /// Lists the tenants for the currently authenticated user.
        /// </summary>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>List of <see cref="Tenant"/> objects describing the tenants of the currently authenticated user.</returns>
        /// <exception cref="NotSupportedException">If the provider does not support the given <paramref name="identity"/> type.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/GET_listTenants_v2.0_tenants_Tenant_Operations.html">List Tenants (OpenStack Identity Service API v2.0 Reference)</seealso>
        IEnumerable<Tenant> ListTenants(CloudIdentity identity = null);

        /// <summary>
        /// Gets the user access details, authenticating with the server if necessary.
        /// </summary>
        /// <remarks>
        /// If <paramref name="forceCacheRefresh"/> is <c>false</c> and a cached <see cref="UserAccess"/>
        /// is available for the specified <paramref name="identity"/>, this method may return the cached
        /// value without performing an authentication against the server. If <paramref name="forceCacheRefresh"/>
        /// is <c>true</c>, this method is equivalent to <see cref="Authenticate"/>.
        /// </remarks>
        /// <param name="identity">The identity of the user to authenticate. If this value is <c>null</c>, the authentication is performed with the <see cref="DefaultIdentity"/>.</param>
        /// <param name="forceCacheRefresh">If <c>true</c>, the user is always authenticated against the server; otherwise a cached <see cref="IdentityToken"/> may be returned.</param>
        /// <returns>A <see cref="UserAccess"/> object containing the authentication token, service catalog and user data.</returns>
        /// <exception cref="NotSupportedException">If the provider does not support the given <paramref name="identity"/> type.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/POST_authenticate_v2.0_tokens_.html">Authenticate (OpenStack Identity Service API v2.0 Reference)</seealso>
        UserAccess GetUserAccess(CloudIdentity identity = null, bool forceCacheRefresh = false);

        /// <summary>
        /// Gets the specified user credential.
        /// </summary>
        /// <param name="userId">The user ID. The behavior is unspecified if this is not obtained from <see cref="User.Id"/> or <see cref="NewUser.Id"/>.</param>
        /// <param name="credentialKey">The credential key.</param>
        /// <param name="identity">The cloud identity to use for this request. If not specified, the <see cref="DefaultIdentity"/> for the current provider instance will be used.</param>
        /// <returns>The <see cref="UserCredential"/> details for the specified credentials type.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="userId"/> is <c>null</c>.
        /// <para>-or-</para>
        /// <para>If <paramref name="credentialKey"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="userId"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="credentialKey"/> is empty.</para>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// If the provider does not support the <see href="http://docs.openstack.org/api/openstack-identity-service/2.0/content/Admin_API_Service_Developer_Operations-d1e1357.html">OS-KSADM Admin Extension</see>.
        /// <para>-or-</para>
        /// <para>If the provider does not support the given <paramref name="identity"/> type.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">If <paramref name="identity"/> is <c>null</c> and no default identity is available for the provider.</exception>
        /// <exception cref="ResponseException">If the REST API request failed.</exception>
        /// <seealso href="">Get User Credentials (OpenStack Identity Service API v2.0 Reference)</seealso>
        UserCredential GetUserCredential(string userId, string credentialKey, CloudIdentity identity = null);

        /// <summary>
        /// Gets the default <see cref="CloudIdentity"/> to use for requests from this provider.
        /// If no default identity is available, the value is <c>null</c>.
        /// </summary>
        CloudIdentity DefaultIdentity { get; }
    }
}
