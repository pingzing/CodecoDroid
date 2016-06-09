using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CodecoDroid
{
    public partial class MainShell : NavigationPage
    {
        public MainShell ()
        {
            InitializeComponent();
        }

        public MainShell(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}
