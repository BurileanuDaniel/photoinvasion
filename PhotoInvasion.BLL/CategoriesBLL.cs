using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoInvasion.DAL;

namespace PhotoInvasion.BLL
{
    public class CategoriesBLL
    {
        CategoriesDAL _access = new CategoriesDAL();

        public List<Category> getCategories()
        {
            return _access.getCategories();
        }
    }
}
