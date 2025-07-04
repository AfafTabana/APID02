﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APID02.Models;

[PrimaryKey("Crs_Id", "St_Id")]
[Table("Stud_Course")]
public partial class Stud_Course
{
    [Key]
    public int Crs_Id { get; set; }

    [Key]
    public int St_Id { get; set; }

    public int? Grade { get; set; }

    [ForeignKey("St_Id")]
    [InverseProperty("Stud_Courses")]
    public virtual Student St { get; set; }
}