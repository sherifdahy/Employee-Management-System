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
        public static SelectorDTO ToDTO(this Selector selector, SelectorDTO selectorDTO)
        {
            if (selector == null || selectorDTO is null) return null;

            selectorDTO.Guid = selector.Guid;
            selectorDTO.Value = selector.Value;
            selectorDTO.ContentType = selector.contentType;
            selectorDTO.SelectorType = selector.selectorType;

            return selectorDTO;
        }

        public static ICollection<SelectorDTO> ToDTO(this ICollection<Selector> selectors, ICollection<SelectorDTO> selectorDTOs)
        {
            if (selectors == null || selectorDTOs == null) return null;

            var dtoDictionary = selectorDTOs.ToDictionary(x => x.Value);

            foreach (var selector in selectors)
            {
                if (!dtoDictionary.TryGetValue(selector.Value, out var dto))
                {
                    // new
                    selectorDTOs.Add(selector.ToDTO(new SelectorDTO()));
                }
                else
                {
                    // exist
                    selector.ToDTO(dto);
                }
            }

            // remove not exist
            foreach (var dto in selectorDTOs.ToList())
            {
                if (!selectors.Any(selector => selector.Value == dto.Value))
                {
                    selectorDTOs.Remove(dto);
                }
            }

            return selectorDTOs;
        }
        #endregion

        #region DTO => Model
        public static Selector ToModel(this SelectorDTO selectorDTO, Selector selector)
        {
            if (selectorDTO is null || selector is null) return null;

            selector.Guid = selectorDTO.Guid;
            selector.Value = selectorDTO.Value;
            selector.contentType = selectorDTO.ContentType;
            selector.selectorType = selectorDTO.SelectorType;

            return selector;
        }

        public static void ToModel(this ICollection<SelectorDTO> selectorDTOs, ICollection<Selector> selectors)
        {
            var selectorsDictionary = selectors.ToDictionary(x => x.Value);

            foreach (var selectorDTO in selectorDTOs)
            {
                if (!selectorsDictionary.TryGetValue(selectorDTO.Value, out var selector))
                {
                    // new
                    selectors.Add(selectorDTO.ToModel(new Selector()));
                }
                else
                {
                    // exist
                    selectorDTO.ToModel(selector);
                }
            }

            foreach (var selector in selectors.ToList())
            {
                // remove not exist
                if (!selectorDTOs.Any(dto => dto.Value == selector.Value))
                {
                    selectors.Remove(selector);
                }
            }
        }
        #endregion
    }

}
