namespace BanHangVip;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

#if WINDOWS
        // Kích thước tỷ lệ 9:16 (Chuẩn HD dọc)
        // Phù hợp với hầu hết màn hình laptop, không bị che mất nút bấm dưới cùng
        window.Width = 405;  
        window.Height = 720; 

        // Căn vị trí cửa sổ
        window.X = 500;
        window.Y = 50; 
#endif

        return window;
    }
}