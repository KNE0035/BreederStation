﻿using BreederStationBussinessLayer.Domain.Enums;
using System;
using System.Text;

namespace BreederStationBussinessLayer.Domain
{
    public class Role
    {

        public int Id { get; set; }
        public RoleEnum Type { get; set; }
        public string Description { get; set; }
    }
}
