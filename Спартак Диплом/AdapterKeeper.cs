using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Спартак_Диплом
{
    class AdapterKeeper
    {
        public static readonly SPEditedDataSetTableAdapters.ClassTableAdapter ClassAdapter;
        public static readonly SPEditedDataSetTableAdapters.GroupPTableAdapter GroupAdapter;
        public static readonly SPEditedDataSetTableAdapters.SubGroupPTableAdapter SubGroupAdapter;
        public static readonly SPEditedDataSetTableAdapters.ProductTableAdapter ProductAdapter;
        public static readonly SPEditedDataSetTableAdapters.ClientTableAdapter ClientAdapter;

        static AdapterKeeper()
        {
            ClassAdapter = new SPEditedDataSetTableAdapters.ClassTableAdapter();
            GroupAdapter = new SPEditedDataSetTableAdapters.GroupPTableAdapter();
            SubGroupAdapter = new SPEditedDataSetTableAdapters.SubGroupPTableAdapter();
            ProductAdapter = new SPEditedDataSetTableAdapters.ProductTableAdapter();
            ClientAdapter = new SPEditedDataSetTableAdapters.ClientTableAdapter();
        }

    }
}
