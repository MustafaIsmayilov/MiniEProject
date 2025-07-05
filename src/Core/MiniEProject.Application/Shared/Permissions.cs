namespace MiniEProject.Application.Shared;

public static class Permissions
{
    public static class Category
    {
        public const string MainCreate = "Category.MainCreate";
        public const string SubCreate = "Category.SubCreate";
        public const string GetAll = "Category.GetAll";
        public const string Update = "Category.Update";
        public const string Delete = "Category.Delete";

        public static List<string> All = new()
        {
            MainCreate,
            SubCreate,
            GetAll,
            Update,
            Delete
        };
    }

    public static class Role
    {
        public const string Create = "Role.Create";
        public const string GetAllPermission = "Role.GetAllPermission";

        public static List<string> All = new()
        {
            Create,
            GetAllPermission
        };
    }

    public static class Account
    {
        public const string AddRole = "Account.AddRole";

        public static List<string> All = new()
        {
            AddRole
        };
    }

}
