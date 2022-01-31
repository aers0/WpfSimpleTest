using System;
using System.ComponentModel.DataAnnotations;

namespace WpfSimpleTest.Models
{
    public class SettingsValue
    {
        public Guid Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Value { get; set; }
        [MaxLength(250)]
        public string DefValue { get; set; }
    }
}
