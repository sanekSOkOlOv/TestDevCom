namespace TestDevCom.Enums
{
    public static class CategoryConfig
    {
        public static readonly Dictionary<Category, List<SubCategory>> Map = new()
        {
            [Category.ПобутоваТехніка] = new()
        {
            SubCategory.Холодильники,
            SubCategory.ПральніМашини,
            SubCategory.Бойлери,
            SubCategory.Печі,
            SubCategory.Витяжки,
            SubCategory.МікрохвильовіПечі
        },
            [Category.КомпютернаТехніка] = new()
        {
            SubCategory.ПК,
            SubCategory.Ноутбуки,
            SubCategory.Монітори,
            SubCategory.Принтери,
            SubCategory.Сканери
        },
            [Category.Смартфони] = new()
        {
            SubCategory.AndroidСмартфони,
            SubCategory.iOS_AppleСмартфони
        },
            [Category.Інше] = new()
        {
            SubCategory.Одяг,
            SubCategory.Взуття,
            SubCategory.Аксесуари,
            SubCategory.СпортивнеОбладнання,
            SubCategory.Іграшки
        }
        };

        public static bool IsSubcategoryValid(Category category, SubCategory subCategory)
        {
            return Map.TryGetValue(category, out var allowed) && allowed.Contains(subCategory);
        }
    }

}
