using System;

namespace InfoSystem.Templates
{
    public class MacroDef<T>
    {
        public string Name { get; private set; }
        public Func<T, string, string> Macro { get; private set; }
        public string Description { get; private set; }

        public MacroDef(string name, Func<T, string, string> macro, string description)
        {
            this.Name = name;
            this.Macro = macro;
            this.Description = description;
        }
    }
}