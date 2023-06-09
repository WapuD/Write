using System;
using System.Collections.Generic;

namespace WapuD.Data.Models_DB;

public partial class User
{
    public int IdUser { get; set; }

    public string LoginUser { get; set; } = null!;

    public string PasswordUser { get; set; } = null!;

    public int RoleUser { get; set; }

    public string NameUser { get; set; } = null!;

    public string SurnameUser { get; set; } = null!;

    public string PatronymicUser { get; set; } = null!;
}
