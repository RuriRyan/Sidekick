using System.Windows.Controls;
using Bindables;

namespace Sidekick.Views.Leagues.Betrayal
{
    /// <summary>
    /// Interaction logic for Reward.xaml
    /// </summary>
    [DependencyProperty]
    public partial class Reward : UserControl
    {
        public Reward()
        {
            InitializeComponent();
            Container.DataContext = this;
            Loaded += Reward_Loaded;
        }

        private void Reward_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Model.Tooltip))
            {
                Container.ToolTip = Model.Tooltip;
            }
        }

        public BetrayalReward Model { get; set; }
    }
}
