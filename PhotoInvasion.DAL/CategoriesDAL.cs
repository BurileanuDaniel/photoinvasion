using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoInvasion.DAL
{
    public class CategoriesDAL
    {
        DatabaseEntities _entities = new DatabaseEntities();

        public List<Category> getCategories()
        {
            return _entities.Category
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
