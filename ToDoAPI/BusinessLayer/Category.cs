using DataAccessLayer;

namespace BusinessLayer
{
    public class Category
    {
        public enum enMode
        {
            addNew = 0,
            Update = 1,
        };

        public enMode Mode = enMode.addNew;

        public CategoryDTO CDTO
        {
            get { return (new CategoryDTO(this.ID, this.name, this.iconName)); }
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string iconName { get; set; }

        public Category(CategoryDTO CDTO, enMode cMode = enMode.addNew)
        {
            this.ID = CDTO.ID;
            this.name = CDTO.name;
            this.iconName = CDTO.iconName;

            this.Mode = cMode;
        }

        public static List<CategoryDTO> GetAllCategories()
        {
            return CategoriesData.GetAllCategories();
        }

        public static Category Find(int id)
        {
            CategoryDTO CDTO = CategoriesData.GetCategoryByID(id);

            if (CDTO != null)
            {
                return new Category(CDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
    }
}
