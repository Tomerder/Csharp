// This time, we had to add a reference to the System.Windows.Forms
//  assembly (DLL) so that we could use the MessageBox class
//  that is defined within it.  To add the reference, we right-clicked
//  on our project in the Solution Explorer, and chose 'Add Reference...'.
//  Then, we scrolled to find System.Windows.Forms in the '.NET' tab
//  of the 'Add Reference' dialog.
//
using System.Windows.Forms;

namespace MessageBoxSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // The MessageBox class defines a static Show method
            //  that displays a graphical message box on the screen
            //  with the specified text.  Note that the method
            //  has various overloads that allow us to control the
            //  window's caption, icon, buttons and more.
            //
            MessageBox.Show("Hello World!");
        }
    }
}
