namespace MiniEProject.Application.Shared;

public static class Permissions
{
    public static class Category
    {
        public const string MainCreate = "Category.MainCreate";
        public const string SubCreate = "Category.SubCreate";
        public const string Update = "Category.Update";
        public const string Delete = "Category.Delete";

        public static List<string> All = new()
        {
            MainCreate,
            SubCreate,
            Update,
            Delete
        };
    }
    public static class Role
    {
        public const string Create = "Role.Create";
        public const string Update = "Role.Update";
        public const string Delete = "Role.Delete";
        public const string GetAllPermissions = "Role.GetAllPermissions";
        public static List<string> All = new List<string>
        {
            GetAllPermissions,
            Create,
            Update,
            Delete

        };
    }
    public static class Account
    {
        public const string AddRole = "Account.AddRole";
        public const string Create = "Account.Create";

        public static List<string> All = new()
        {
            AddRole,
            Create
        };
    }

    public static class Product
    {
        public const string Create = "Product.Create";
        public const string Update = "Product.Update";
        public const string Delete = "Product.Delete";
        public const string GetMy = "Product.GetMy";
        public const string DeleteProductImage = "Product.DeleteImage";
        public const string AddProductImage = "Product.AddImage";
        public const string AddFavourite = "Product.AddFavourite";

        public static List<string> All = new List<string>
        {
            Create,
            Update,
            Delete,
            GetMy,
            DeleteProductImage,
            AddProductImage,
            AddFavourite
        };
    }
    public static class Order
    {
        public const string Create = "Order.Create";
        public const string GetMySales = "Order.GetMySales";
        public const string GetDetail = "Order.GetDetail";
        public const string Update = "Order.Update";
        public const string Delete = "Order.Delete";
        public const string GetMy = "Order.GetMy";
        public const string GetAll = "Order.GetAll";


        public static List<string> All = new()
        {
            Create,
            Update,
            GetMySales,
            GetDetail,
            Delete,
            GetMy,
            GetAll
        };
    }
    public static class User
    {

        public const string ResetPaswword = "User.ResetPassword";
        public const string GetAll = "User.GetAll";
        public const string GetById = "User.GetById";
        public const string Create = "User.Create";

        public static List<string> All = new()
        {
            GetAll,
            GetById,
            ResetPaswword,
            Create
        };
    }
    public static class Review
    {
        public const string Create = "Review.Create";
        public const string Delete = "Review.Delete";

        public static List<string> All = new()
        {
            Create,
            Delete
        };
    }
}
