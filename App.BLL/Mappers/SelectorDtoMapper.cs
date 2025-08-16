using App.BLL.DTOs;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Mappers
{
    public static class SelectorDtoMapper
    {
        #region Model => DTO
        public static SelectorDTO ToDTO(this Selector selector)
        {
            if (selector == null) return null;

            return new SelectorDTO()
            {
                Id = selector.Id,
                Value = selector.Value,
                ContentType = selector.contentType,
                SelectorType = selector.selectorType,
            };
        }
        public static IEnumerable<SelectorDTO> ToDTO(this IEnumerable<Selector> selectors)
        {
            if (selectors == null) throw new ArgumentNullException(nameof(selectors));

            return selectors.Select(x => ToDTO(x));
        }
        #endregion


        #region DTO => Model
        public static Selector ToModel(this SelectorDTO selectorDTO)
        {
            if (selectorDTO == null) return null;
            return new Selector()
            {
                Id = selectorDTO.Id,
                Value = selectorDTO.Value,
                contentType = selectorDTO.ContentType,
                selectorType = selectorDTO.SelectorType,
            };
        }
        
        public static void ToModel (this SelectorDTO selectorDTO,Selector selector)
        {
            selector.Value = selectorDTO.Value;
            selector.contentType = selector.contentType;
            selector.selectorType = selectorDTO.SelectorType;
        }

        public static List<Selector> ToModel(this List<SelectorDTO> selectorsDTO)
        {
            return selectorsDTO.Select(x => ToModel(x)).ToList();
        }
        #endregion
    }
}
