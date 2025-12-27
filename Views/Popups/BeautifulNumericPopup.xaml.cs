using CommunityToolkit.Maui.Views;
using System.Globalization;

namespace BanHangVip.Views.Popups;

public partial class BeautifulNumericPopup : Popup
{
    private string _currentInput = "0";

    public BeautifulNumericPopup(string itemName)
    {
        InitializeComponent();
        ItemNameLabel.Text = itemName;
        UpdateDisplay();
    }

    private void OnNumberClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        string pressed = button.Text;

        if (pressed == ".")
        {
            if (_currentInput.Contains(".")) return;
            if (_currentInput == "0") _currentInput = "0.";
            else _currentInput += ".";
        }
        else
        {
            if (_currentInput == "0") _currentInput = pressed;
            else _currentInput += pressed;
        }
        UpdateDisplay();
    }

    private void OnBackspaceClicked(object sender, EventArgs e)
    {
        if (_currentInput.Length > 1)
        {
            _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
            if (_currentInput.EndsWith(".")) // Xóa luôn d?u ch?m n?u nó ? cu?i
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
        }
        else
        {
            _currentInput = "0";
        }
        UpdateDisplay();
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        _currentInput = "0";
        UpdateDisplay();
    }

    private void OnConfirmClicked(object sender, EventArgs e)
    {
        // S? d?ng CultureInfo.InvariantCulture ð? ð?m b?o d?u ch?m luôn là d?u th?p phân
        if (double.TryParse(_currentInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
        {
            Close(result);
        }
        else
        {
            Close(0d);
        }
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Close(null);
    }

    private void UpdateDisplay()
    {
        DisplayLabel.Text = _currentInput;
    }
}