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
        public static Selector ToModel(this SelectorViewModel selectorViewModel,Selector selector)
        {
            if (selectorViewModel == null) return null;

            selector.Guid = selectorViewModel.Guid;
            selector.Value = selectorViewModel.Value;
            selector.selectorType = selectorViewModel.SelectorType;
            selector.contentType = selectorViewModel.ContentType;

            return selector;
        }

        public static ICollection<Selector> ToModel(this IEnumerable<SelectorViewModel> selectorViewModels,ICollection<Selector> selectors)
        {
            if (selectorViewModels == null || selectors is null) return null;

            var selectorsDictionary = selectors.ToDictionary(x => x.Guid);
            
            foreach (var selectorViewModel in selectorViewModels)
            {
                if(!selectorsDictionary.TryGetValue(selectorViewModel.Guid,out Selector selector))
                {
                    // new
                    selectors.Add(selectorViewModel.ToModel(new Selector()));
                }
                else
                {
                    // exist
                    selectorViewModel.ToModel(selector);
                }
            }

            foreach(var selector in selectors)
            {
                if(!selectorViewModels.Any(X=>X.Guid == selector.Guid))
                {
                    selectors.Remove(selector);
                }
            }


            return selectors;
        }

        #endregion

        #region Model => ViewModel
        public static SelectorViewModel ToViewModel(this Selector selector,SelectorViewModel selectorViewModel)
        {
            if (selector == null || selectorViewModel is null) return null;

            selectorViewModel.Value = selector.Value;
            selectorViewModel.ContentType = selector.contentType;
            selectorViewModel.SelectorType = selector.selectorType;

            return selectorViewModel;
        }

        public static ICollection<SelectorViewModel> ToViewModel(this IEnumerable<Selector> selectors,ICollection<SelectorViewModel> selectorViewModels)
        {
            if(selectors is null || selectorViewModels is null) return null;

            var selectorsVmsDictionary = selectorViewModels.ToDictionary(x => x.Guid);

            foreach (var selector in selectors)
            {
                if (!selectorsVmsDictionary.TryGetValue(selector.Guid, out SelectorViewModel selectorVM))
                {
                    // new
                    selectorViewModels.Add(selector.ToViewModel(new SelectorViewModel()));
                }
                else
                {
                    // exist
                    selector.ToViewModel(selectorVM);
                }
            }

            foreach (var selectorVM in selectorViewModels)
            {
                if (!selectors.Any(X => X.Guid == selectorVM.Guid))
                {
                    selectorViewModels.Remove(selectorVM);
                }
            }

            return selectorViewModels;
        }
        #endregion

    }
}
