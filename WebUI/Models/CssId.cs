using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumium.Photos.WebUI.Models
{
    public class CssId
    {
        public string Id { get; set; } = String.Empty;
        public string Selector
        {
            get => "#" + Id;
        }

        public override string ToString()
        {
            return Id;
        }

        public CssId()
        {
            Id = $"wui-{Guid.NewGuid()}";
        }

        public static CssId New() => new();

        public static implicit operator String(CssId id)
        {
            return id.ToString();
        }
        public static implicit operator CssId(string id)
        {
            CssId cssId = new CssId();
            cssId.Id = id;
            return cssId;
        }
    }
}
