using System.ComponentModel;

namespace RPM_X_2.Models
{
    public enum RoleEnum
    {
        [Description("Клиент")]
        Client,
        [Description("Библиотекарь")]
        Libranian,
        [Description("Администратор")]
        Administrator
    }
}