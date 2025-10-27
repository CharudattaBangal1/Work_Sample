using System;
using System.Collections.Generic;

namespace omnisearch
{
    public class TreeNode1
    {
        public string id { get; set; }

        public string text { get; set; }

        public string icon { get; set; } // You can add "icon" property if needed

        public List<TreeNode1> children { get; set; }

    }

    public class Mpn1
    {
        public TreeNode1 documents { get; set; }
    }

    public class BasicDetails1
    {
        public TreeNode1 basicDetails { get; set; }
    }

    public class Endorsement1
    {
        public TreeNode1 endorsements { get; set; }
    }

    public class Endt_Ecard1
    {
        public TreeNode1 Endt_ECards { get; set; }
    }

    public class BasicDetails_Endorsement1
    {
        public TreeNode1 EE_Endt_Schedule { get; set; }
        public TreeNode1 EE_Endt_Annexure { get; set; }
        public TreeNode1 EE_Endt_CD_Statement { get; set; }
    }
}
