// SunamoWpfControlsToggleSwitch
namespace SunamoWpf.Controls.ToggleSwitch;

public partial class HorizontalToggleSwitchWithLabel : UserControl
{
    /// <summary>
    /// Initializes a new instance of the horizontal toggle switch with label class.
    /// </summary>
    public HorizontalToggleSwitchWithLabel()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception)
        {
#if DEBUG
            Debugger.Break();
#endif
        }
    }

    /// <summary>
    /// The label.
    /// </summary>
    public string Label
    {
        set => tb.Text = value;
    }
}
