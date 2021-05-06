using AutoMapper;
using BusinessAccessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessAccessLayer.DragonService
{
    public class DragonService : IDragonService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public DragonService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void CreateDaragon(string quantityDragon)
        {
            if (int.TryParse(quantityDragon, out int quantity))
            {
                for (int i = 0; i < quantity; i++)
                {
                    DragonDTO dragon = new DragonDTO();
                    var tempDragon = _mapper.Map<Dragon>(dragon);
                    _dbContext.Dragons.Add(tempDragon);
                }

                _dbContext.SaveChanges();
            }
        }

        public PageResult<DragonDTO> SearchHP(int? page, string textSearch, string paramsFilter, int pagesize)
        {
            if(int.TryParse(textSearch, out int HP))
            {
                if(paramsFilter == "more")
                {
                    var tempDragon = _dbContext.Dragons
                            .Where(n => n.HP > HP)
                            .ToList();
                    var tempHit = _dbContext.Hits.ToList();

                    List<DragonDTO> resultList = new List<DragonDTO>();


                    foreach (var dragon in tempDragon)
                    {
                        if (tempHit.FirstOrDefault(i => i.DragonId == dragon.Id) != null)
                        {
                            int remnant = dragon.HP;

                            foreach (var dragonHit in tempHit)
                            {
                                if (dragonHit.DragonId == dragon.Id)
                                {
                                    if (remnant >= dragonHit.ImpactForce)
                                    {
                                        remnant -= dragonHit.ImpactForce;
                                    }
                                    else
                                    {
                                        remnant = 0;
                                    }
                                }
                            }

                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = remnant;
                            resultList.Add(dragonItem);
                        }
                        else
                        {
                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = dragonItem.HP;
                            resultList.Add(dragonItem);
                        }
                    }

                    var allItemCount = resultList.Count();

                    var result = new PageResult<DragonDTO>
                    {
                        Count = allItemCount,
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = resultList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };

                    return result;
                }
                else
                {
                    var tempDragon = _dbContext.Dragons
                            .Where(n => n.HP < HP)
                            .ToList();
                    var tempHit = _dbContext.Hits.ToList();

                    List<DragonDTO> resultList = new List<DragonDTO>();


                    foreach (var dragon in tempDragon)
                    {
                        if (tempHit.FirstOrDefault(i => i.DragonId == dragon.Id) != null)
                        {
                            int remnant = dragon.HP;

                            foreach (var dragonHit in tempHit)
                            {
                                if (dragonHit.DragonId == dragon.Id)
                                {
                                    if (remnant >= dragonHit.ImpactForce)
                                    {
                                        remnant -= dragonHit.ImpactForce;
                                    }
                                    else
                                    {
                                        remnant = 0;
                                    }
                                }
                            }

                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = remnant;
                            resultList.Add(dragonItem);
                        }
                        else
                        {
                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = dragonItem.HP;
                            resultList.Add(dragonItem);
                        }
                    }

                    var allItemCount = resultList.Count();

                    var result = new PageResult<DragonDTO>
                    {
                        Count = allItemCount,
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = resultList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };

                    return result;
                }
            }
            else
            {
                return null;
            }
        }
        public PageResult<DragonDTO> SearchRemnant(int? page, string textSearch, string paramsFilter, int pagesize)
        {
            if(int.TryParse(textSearch, out int Remnant))
            {
                if(paramsFilter == "more")
                {
                    var tempDragon = _dbContext.Dragons.ToList();
                    var tempHit = _dbContext.Hits.ToList();

                    List<DragonDTO> resultList = new List<DragonDTO>();


                    foreach (var dragon in tempDragon)
                    {
                        if (tempHit.FirstOrDefault(i => i.DragonId == dragon.Id) != null)
                        {
                            int remnant = dragon.HP;

                            foreach (var dragonHit in tempHit)
                            {
                                if (dragonHit.DragonId == dragon.Id)
                                {
                                    if (remnant >= dragonHit.ImpactForce)
                                    {
                                        remnant -= dragonHit.ImpactForce;
                                    }
                                    else
                                    {
                                        remnant = 0;
                                    }
                                }
                            }

                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = remnant;
                            resultList.Add(dragonItem);
                        }
                        else
                        {
                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = dragonItem.HP;
                            resultList.Add(dragonItem);
                        }
                    }

                    var allItemCount = resultList.Count();
                    var tempList = resultList.Where(r => r.Remnant > Remnant).ToList();
                    var result = new PageResult<DragonDTO>
                    {
                        Count = tempList.Count(),
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };

                    return result;
                }
                else
                {
                    var tempDragon = _dbContext.Dragons.ToList();
                    var tempHit = _dbContext.Hits.ToList();

                    List<DragonDTO> resultList = new List<DragonDTO>();


                    foreach (var dragon in tempDragon)
                    {
                        if (tempHit.FirstOrDefault(i => i.DragonId == dragon.Id) != null)
                        {
                            int remnant = dragon.HP;

                            foreach (var dragonHit in tempHit)
                            {
                                if (dragonHit.DragonId == dragon.Id)
                                {
                                    if (remnant >= dragonHit.ImpactForce)
                                    {
                                        remnant -= dragonHit.ImpactForce;
                                    }
                                    else
                                    {
                                        remnant = 0;
                                    }
                                }
                            }

                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = remnant;
                            resultList.Add(dragonItem);
                        }
                        else
                        {
                            DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                            dragonItem.Remnant = dragonItem.HP;
                            resultList.Add(dragonItem);
                        }
                    }

                    var allItemCount = resultList.Count();
                    var tempList = resultList.Where(r => r.Remnant < Remnant).ToList();
                    var result = new PageResult<DragonDTO>
                    {
                        Count = tempList.Count(),
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };

                    return result;
                }
            }
            else
            {
                return null;
            }
        }
        public PageResult<DragonDTO> SearchName(int? page, string textSearch, string paramsFilter, int pagesize)
        {
            if (int.TryParse(paramsFilter, out int filter))
            {
               var tempDragon = _dbContext.Dragons
                        .Where(n => n.Name.Length >= filter && n.Name.StartsWith(textSearch))
                        .ToList();
                var tempHit = _dbContext.Hits.ToList();

                List<DragonDTO> resultList = new List<DragonDTO>();


                foreach (var dragon in tempDragon)
                {
                    if (tempHit.FirstOrDefault(i => i.DragonId == dragon.Id) != null)
                    {
                        int remnant = dragon.HP;

                        foreach (var dragonHit in tempHit)
                        {
                            if (dragonHit.DragonId == dragon.Id)
                            {
                                if (remnant >= dragonHit.ImpactForce)
                                {
                                    remnant -= dragonHit.ImpactForce;
                                }
                                else
                                {
                                    remnant = 0;
                                }
                            }
                        }

                        DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                        dragonItem.Remnant = remnant;
                        resultList.Add(dragonItem);
                    }
                    else
                    {
                        DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                        dragonItem.Remnant = dragonItem.HP;
                        resultList.Add(dragonItem);
                    }
                }

                var allItemCount = resultList.Count();

                var result = new PageResult<DragonDTO>
                {
                    Count = allItemCount,
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                };

                return result;
            }
            return null;
        }
        public PageResult<DragonDTO> GetDragon(int? page, int pagesize)
        {
            var tempDragon = _dbContext.Dragons.ToList();
            var tempHit = _dbContext.Hits.ToList();

            List<DragonDTO> resultList = new List<DragonDTO>();

            foreach (var dragon in tempDragon)
            {
                if (tempHit.FirstOrDefault(i => i.DragonId == dragon.Id) != null)
                {
                    int remnant = dragon.HP;

                    foreach (var dragonHit in tempHit)
                    {
                        if (dragonHit.DragonId == dragon.Id)
                        {
                            if (remnant >= dragonHit.ImpactForce)
                            {
                                remnant -= dragonHit.ImpactForce;
                            }
                            else
                            {
                                remnant = 0;
                            }
                        }
                    }

                    DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                    dragonItem.Remnant = remnant;
                    resultList.Add(dragonItem);
                }
                else
                {
                    DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                    dragonItem.Remnant = dragonItem.HP;
                    resultList.Add(dragonItem);
                }
            }

            var allItemCount = resultList.Count();

            var result = new PageResult<DragonDTO>
            {
                Count = allItemCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resultList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };

            return result;
        }

        public PageResult<DragonDTO> GetDragonSort(int? page, string paramsSort, int pagesize)
        {
            var tempDragon = _dbContext.Dragons.ToList();
            var tempHit = _dbContext.Hits.ToList();

            List<DragonDTO> resultList = new List<DragonDTO>();

            foreach (var dragon in tempDragon)
            {
                if (tempHit.FirstOrDefault(i => i.DragonId == dragon.Id) != null)
                {
                    int remnant = dragon.HP;

                    foreach (var dragonHit in tempHit)
                    {
                        if (dragonHit.DragonId == dragon.Id)
                        {
                            if (remnant >= dragonHit.ImpactForce)
                            {
                                remnant -= dragonHit.ImpactForce;
                            }
                            else
                            {
                                remnant = 0;
                            }
                        }
                    }

                    DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                    dragonItem.Remnant = remnant;
                    resultList.Add(dragonItem);
                }
                else
                {
                    DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                    dragonItem.Remnant = dragonItem.HP;
                    resultList.Add(dragonItem);
                }
            }

            if (paramsSort == "ASC")
            {
                var allItemCount = resultList.Count();
                var tempList = resultList.OrderBy(n => n.Name);
                var result = new PageResult<DragonDTO>
                {
                    Count = tempList.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                };

                return result;
            }
            else
            {
                var allItemCount = resultList.Count();
                var tempList = resultList.OrderByDescending(n => n.Name);
                var result = new PageResult<DragonDTO>
                {
                    Count = tempList.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = tempList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                };

                return result;
            }
        }

        public PageResult<DragonDTO> GetSearchId(int? page, string idDragon, int pagesize)
        {
            var tempDragon = _dbContext.Dragons.ToList();
            var tempHit = _dbContext.Hits.ToList();

            List<DragonDTO> resultList = new List<DragonDTO>();
            if (idDragon != null)
            {
                if (int.TryParse(idDragon, out int id_Dragon))
                {
                    foreach (var dragon in tempDragon)
                    {
                        if (dragon.Id == id_Dragon)
                        {
                            if (tempHit.FirstOrDefault(i => i.DragonId == id_Dragon) != null)
                            {
                                int remnant = dragon.HP;

                                foreach (var dragonHit in tempHit)
                                {
                                    if (dragonHit.DragonId == dragon.Id)
                                    {
                                        if (remnant >= dragonHit.ImpactForce)
                                        {
                                            remnant -= dragonHit.ImpactForce;
                                        }
                                        else
                                        {
                                            remnant = 0;
                                        }
                                    }
                                }

                                DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                                dragonItem.Remnant = remnant;
                                resultList.Add(dragonItem);
                            }
                            else
                            {
                                DragonDTO dragonItem = _mapper.Map<DragonDTO>(dragon);
                                dragonItem.Remnant = dragonItem.HP;
                                resultList.Add(dragonItem);
                            }
                            break;
                        }
                    }
                    var allItemCount = resultList.Count();

                    var result = new PageResult<DragonDTO>
                    {
                        Count = allItemCount,
                        PageIndex = page ?? 1,
                        PageSize = pagesize,
                        Items = resultList.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
                    };

                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}


