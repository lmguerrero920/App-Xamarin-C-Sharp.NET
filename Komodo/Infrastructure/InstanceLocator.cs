using Komodo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komodo.Infrastructure
{
    



    public class InstanceLocator
    {

        #region Propiertis
        public MainViewModel Main 
        { get;
          set;
        }

        #endregion


        #region Constructor
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
