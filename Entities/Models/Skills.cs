﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Skills
    {
        [Key]
        public int SkillId { get; set; }
        public string SkillTitle { get; set; }
        public string SkillDescription { get; set; }
        public string SkillImage { get ; set; }
    }
}