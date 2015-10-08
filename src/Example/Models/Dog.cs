﻿using System;
using System.Collections.Generic;
using UIOMatic.Atributes;
using UIOMatic.Enums;
using UIOMatic.Interfaces;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Example.Models
{
    [UIOMatic("Dogs", "icon-users", "icon-user" )]
    [TableName("Dogs")]
    public class Dog : IUIOMaticModel
    {
        public Dog() { }

        [UIOMaticIgnoreField]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }


        public string Name { get; set; }

        //public DateTime BirthDay { get; set; }


        [UIOMaticField("Is castrated", "Has the dog been castrated")]
        public bool IsCastrated { get; set; }


        [UIOMaticField("Owner", "Select the owner of the dog", View = "~/App_Plugins/Example/picker.person.html")]
        public int OwnerId { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<Exception> Validate()
        {
            var exs = new List<Exception>();

            if (string.IsNullOrEmpty(Name))
                exs.Add(new Exception("Please provide a value for name"));

            if (OwnerId == 0)
                exs.Add(new Exception("Please select an owner"));


            return exs;
        }
    }
}