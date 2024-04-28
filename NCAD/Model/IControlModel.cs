using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NCAD.Model
{
    public interface IControlModel
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public Point Position { get; set; }
        public Point Size { get; set; }

        public void Translate(float x, float y);
        public void Scale(float x, float y);
    }
}
