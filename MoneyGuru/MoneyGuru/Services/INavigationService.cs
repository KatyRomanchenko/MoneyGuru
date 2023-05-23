using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyGuru 
{
    public interface INavigationService
    {
        void NavigateToPage(MainPageItem item);

        void NavigateToPage(Type item, object parameter);

        void NavigatePopToRoot();
    }
}
