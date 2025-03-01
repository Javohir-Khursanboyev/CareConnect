﻿using CareConnect.Domain.Commons;

namespace CareConnect.Domain.Entities.Users;

public class Permission : Auditable
{
    public string Action { get; set; }
    public string Controller { get; set; }

    public IEnumerable<RolePermission> RolePermissions { get; set; }
}
