﻿using SondageSoiree.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SondageSoiree.Models.Entity
{
    [MetadataType(typeof(RestaurantMetadata))]
    public partial class Restaurant
    {
    }
}