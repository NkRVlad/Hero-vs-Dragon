using BusinessAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.DragonService
{
    public interface IDragonService
    {
        public void CreateDaragon(string quantityDragon);
        PageResult<DragonDTO> GetDragon(int? page, int pagesize = 30);
        PageResult<DragonDTO> GetSearchId(int? page, string idDragon, int pagesize = 30);
        PageResult<DragonDTO> GetDragonSort(int? page, string paramsSort, int pagesize = 30);
        PageResult<DragonDTO> SearchName(int? page, string textSearch, string paramsFilter, int pagesize = 30);
         PageResult<DragonDTO> SearchHP(int? page, string textSearch, string paramsFilter, int pagesize = 30);
         PageResult<DragonDTO> SearchRemnant(int? page, string textSearch, string paramsFilter, int pagesize = 30);
         
        
    }
}
