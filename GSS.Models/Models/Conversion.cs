using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace GSS.Models.Models
{
    
    
    public static class Conversion
    {
        public static TModel ConvertToObject<TModel>(this object entity) where TModel : class
        {
            Mapper.CreateMap(entity.GetType(), typeof(TModel));
            return (TModel)Mapper.Map(entity, entity.GetType(), typeof(TModel));
        }

        public static TModel ConvertToObjects<TModel>(this object entity) where TModel : class
        {
            try
            {
                Mapper.CreateMap(entity.GetType().GetGenericArguments()[0], typeof(TModel).GetGenericArguments()[0]);
                return (TModel)Mapper.Map(entity, entity.GetType(), typeof(TModel));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}


