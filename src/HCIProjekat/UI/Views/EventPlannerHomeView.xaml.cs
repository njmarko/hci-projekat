using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Controls;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for EventPlannerHomeView.xaml
    /// </summary>
    public partial class EventPlannerHomeView : UserControl
    {
        private TaskCardModel _currentTask = null;

        public EventPlannerHomeView()
        {
            InitializeComponent();
        }

        private void OnTaskMouseDown(object sender, MouseButtonEventArgs e)
        {
            var task = sender as EventPlannerTaskCard;
           _currentTask = task.DataContext as TaskCardModel;
            DragDrop.DoDragDrop(this, "nista", DragDropEffects.Move);
        }

        private void OnToDoTaskDrop(object sender, DragEventArgs e)
        {
            if (_currentTask == null)
            {
                return;
            }
            _currentTask.MoveToDo.Execute(null);
            _currentTask = null;
        }

        private void OnInProgressTaskDrop(object sender, DragEventArgs e)
        {
            if (_currentTask == null)
            {
                return;
            }
            _currentTask.MoveInProgress.Execute(null);
            _currentTask = null;
        }

        private void OnSentToClientTaskDrop(object sender, DragEventArgs e)
        {
            if (_currentTask == null)
            {
                return;
            }
            _currentTask.MoveSentToClient.Execute(null);
            _currentTask = null;
        }

    }
}
