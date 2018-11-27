using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MikiDB_GUI {
    public static class Commands {
		public static readonly RoutedUICommand RunSQL = new RoutedUICommand (
            "RunSQL",
            "RumnSQL",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Enter, ModifierKeys.None)
            }
        );
		public static readonly RoutedUICommand GetHistBack = new RoutedUICommand (
            "GetHistBack",
            "GetHistBack",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Up, ModifierKeys.Alt)
            }
        );
		public static readonly RoutedUICommand GetHistForw = new RoutedUICommand (
            "GetHistForw",
            "GetHistForw",
            typeof(Commands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.Down, ModifierKeys.Alt)
            }
        );
	}
}
