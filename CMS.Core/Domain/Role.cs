using System;
using System.Collections;
using System.Text;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Summary description for Role.
    /// </summary>
    public class Role
    {
        private int _id;
        private string _name;
        private int _permissionLevel;
        private AccessLevel[] _permissions;
        private DateTime _updateTimestamp;
        private IList _users;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Role()
        {
            _id = -1;
            _name = null;
            _permissionLevel = -1;
        }

        /// <summary>
        /// Property Id (int)
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Property Name (string)
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Property PermissionLevel (int). When set, the integer value is translated to a list of 
        /// AccessLevel enums (Permissions).
        /// </summary>
        public virtual int PermissionLevel
        {
            get { return _permissionLevel; }
            set
            {
                _permissionLevel = value;
                TranslatePermissionLevelToAccessLevels();
            }
        }

        /// <summary>
        /// Gets a list of translated AccessLevel enums of the Role.
        /// </summary>
        public virtual AccessLevel[] Permissions
        {
            get { return _permissions; }
        }

        public virtual string PermissionsString
        {
            get { return GetPermissionsAsString(); }
        }

        /// <summary>
        /// Property UpdateTimestamp (DateTime)
        /// </summary>
        public virtual DateTime UpdateTimestamp
        {
            get { return _updateTimestamp; }
            set { _updateTimestamp = value; }
        }

        /// <summary>
        /// Check if the role has the requested access rights.
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public virtual bool HasPermission(AccessLevel permission)
        {
            return Array.IndexOf(Permissions, permission) > -1;
        }

        private void TranslatePermissionLevelToAccessLevels()
        {
            ArrayList permissions = new ArrayList();
            AccessLevel[] accessLevels = (AccessLevel[]) Enum.GetValues(typeof (AccessLevel));

            foreach (AccessLevel accesLevel in accessLevels)
            {
                if ((PermissionLevel & (int) accesLevel) == (int) accesLevel)
                {
                    permissions.Add(accesLevel);
                }
            }
            _permissions = (AccessLevel[]) permissions.ToArray(typeof (AccessLevel));
        }

        private string GetPermissionsAsString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _permissions.Length; i++)
            {
                AccessLevel accessLevel = _permissions[i];
                sb.Append(accessLevel.ToString());
                if (i < _permissions.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        public virtual IList Users
        {
            get { return _users; }
            set { _users = value; }
        }

        /// <summary>
        /// Filter the anonymous roles from the list.
        /// </summary>
        public static IList FilterAnonymousRoles(IList roles)
        {
            int roleCount = roles.Count;
            for (int i = roleCount - 1; i >= 0; i--)
            {
                Role role = (Role)roles[i];
                if (role.PermissionLevel == (int)AccessLevel.Anonymous)
                {
                    roles.Remove(role);
                }
            }
            return roles;
        }
    }
}