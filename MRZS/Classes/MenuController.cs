using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MRZS.Classes.DisplayCode;
using MRZS.Web.Models;

namespace MRZS.Classes
{
    internal class MenuController
    {
        IEnumerable<mrzs05mMenu> MrzsTables;        
        DisplayViewModel dispControllr = new DisplayViewModel();
        LoadData ld = new LoadData();
        List<int?> SelectedID = new List<int?>(0);
        List<int?> AllParentID;

        public event EventHandler DataLoad;        
        int? CurrentParentID = null;
        

        internal MenuController()
        {
            //subcribe to data load event
            ld.DataLoaded += ld_DataLoaded;
        }

        void ld_DataLoaded(object sender, EventArgs e)
        {
            if (DataLoad != null) DataLoad(this, EventArgs.Empty);
        }

        internal List<Menu> getFirstMenu()
        {
            AllParentID = AddFunctions.selectDistinctMrzsId(ld.MrzsTable);
            CurrentParentID = AllParentID[0];
            return getMenu(CurrentParentID);            
        }
        List<Menu> getMenu(int? parentID)
        {
            List<mrzs05mMenu> mrzsTables= AddFunctions.getEntitiesByParentID(ld.MrzsTable, parentID);
            return getTransformedMenu(mrzsTables);
        }
        List<Menu> getTransformedMenu(List<mrzs05mMenu> MrzsTables)
        {
            List<Menu> list = new List<Menu>(0);
            Menu m;
            foreach (mrzs05mMenu entity in MrzsTables)
            {
                m = new Menu();
                //set ids
                m.ID = entity.id;
                m.ParentID = entity.parentID;
                //check column
                if (entity.menuElement.IndexOf("{value}") != -1)
                {
                }
                else if (entity.menuElement.IndexOf("{0000.0000}") != -1)
                {
                }
                else if (entity.menuElement.IndexOf("{dd:mm:year\\hh:mm:ss}") != -1)
                {
                }
                else if(entity.menuElement==null)
                {

                }
                else
                {
                    m.Name = entity.menuElement;
                }
                list.Add(m);
            }
            return list;
        }
        internal DisplayViewModel setDefaultMenu()
        {
            //show first level menu
            return dispControllr.showMenu(getFirstMenu());
        }
        internal void showNextMenuLine()
        {
            dispControllr.moveToNextLine();
        }

    }
}
