using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for DemoView.xaml
    /// </summary>
    public partial class DemoView : UserControl
    {
		/*public DemoView()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            player.Play();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (player.Source != null)
            {
                if (player.NaturalDuration.HasTimeSpan)
                    label.Text = String.Format("{0} / {1}", player.Position.ToString(@"mm\:ss"), player.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                label.Text = "No file selected...";
        }
        private void play_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

     */

		private bool mediaPlayerIsPlaying = false;
		private bool userIsDraggingSlider = false;

		public DemoView()
		{
			InitializeComponent();

			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();
			mePlayer.Play();
			mediaPlayerIsPlaying = true;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
			{
				sliProgress.Minimum = 0;
				sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
				sliProgress.Value = mePlayer.Position.TotalSeconds;
			}
		}

	

		private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
		}

		private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			mePlayer.Play();
			mediaPlayerIsPlaying = true;
		}

		private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = mediaPlayerIsPlaying;
		}

		private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			mePlayer.Pause();
			mediaPlayerIsPlaying = false;
		}

		private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = mediaPlayerIsPlaying;
		}

		private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			mePlayer.Stop();
			mediaPlayerIsPlaying = false;
		}

		private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
		{
			userIsDraggingSlider = true;
		}

		private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			userIsDraggingSlider = false;
			mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
		}

		private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
		}

		private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
		}

	}
}
