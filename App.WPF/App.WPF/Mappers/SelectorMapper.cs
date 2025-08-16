using App.BLL.DTOs;
using App.Entities.Models;
using MyApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Mappers
{
    public static class SelectorMapper
    {
        #region ViewModel => Model
        public static Selector ToModel(this SelectorViewModel selectorViewModel)
        {
            if (selectorViewModel == null) throw new ArgumentNullException(nameof(selectorViewModel));

            return new Selector()
            {
                Value = selectorViewModel.Value,
                selectorType = selectorViewModel.SelectorType,
                contentType = selectorViewModel.ContentType,
            };
        }

        public static IEnumerable<Selector> ToModel(this IEnumerable<SelectorViewModel> selectorViewModels)
        {
            if (selectorViewModels == null) throw new ArgumentNullException(nameof(selectorViewModels));

            return selectorViewModels.Select(x => ToModel(x));
        }

        #endregion

        #region Model => ViewModel
        public static SelectorViewModel ToViewModel(this Selector selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return new SelectorViewModel()
            {
                Value = selector.Value,
                ContentType = selector.contentType,
                SelectorType = selector.selectorType
            };
        }

        public static IEnumerable<SelectorViewModel> ToViewModel(this IEnumerable<Selector> selectors)
        {
            if (selectors == null) throw new ArgumentNullException(nameof(selectors));

            return selectors.Select(x=> ToViewModel(x));
        }

        #endregion

    }
}
