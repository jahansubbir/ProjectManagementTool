﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectManagementTool.Models
{
    public class Priority
    {
        public int Id { get; set; }
        [DisplayName("Priority")]
        public string Name { get; set; }

    }
}